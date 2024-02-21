using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SastWiki.Core.Contracts.Infrastructure;
using SastWiki.Core.Contracts.Infrastructure.CacheService;
using SastWiki.Core.Contracts.Infrastructure.SettingsService;
using SastWiki.Core.Models;

namespace SastWiki.Core.Services.Infrastructure.CacheService
{
    public class CacheStorage : ICacheStorage
    {
        public CacheStorage(ISettingsProvider settings, ILocalStorage storage)
        {
            _settings = settings;
            _storage = storage;
            InitializeTask = InitializeAsync();
        }

        Task InitializeTask;
        readonly string _cachePath = "D:\\cache";
        readonly ILocalStorage _storage;
        readonly ISettingsProvider _settings;
        List<CacheFile> _cacheList = [];
        Dictionary<string, ReaderWriterLockSlim> _locks = [];

        async Task InitializeAsync()
        {
            try
            {
                List<CacheFile> cachefilelist;
                try
                {
                    cachefilelist = await _settings.GetItem<List<CacheFile>>("CacheList") ?? [];
                }
                catch (Exception)
                {
                    cachefilelist = [];
                    await _settings.SetItem("CacheList", cachefilelist);
                }
                if (cachefilelist is not null)
                {
                    _cacheList = cachefilelist;
                    lock (_locks)
                        foreach (var cache in cachefilelist)
                        {
                            _locks.Add(cache.FileName, new());
                        }
                }
                else
                {
                    _cacheList = [];
                }
            }
            catch (Exception e)
            {
                throw new Exception("Cache Storage Init Fail", e);
            }
        }

        public Task ClearExpiredCacheAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ContainsAsync(string ID)
        {
            await InitializeTask;
            return _cacheList.Exists(x => x.FileName == ID)
                && _locks.ContainsKey(ID)
                && await _storage.Contains(_cachePath, ID);
        }

        public async Task<string> CreateCacheFileAsync(TimeSpan expireTime)
        {
            await InitializeTask;

            // Create a random cache file
            var randomName = Guid.NewGuid().ToString();
            await _storage.CreateAsync(_cachePath, randomName);
            lock (_cacheList)
            {
                _cacheList.Add(
                    new CacheFile()
                    {
                        FileName = randomName,
                        ExpireTime = expireTime,
                        UpdatedTime = DateTime.Now
                    }
                );
                lock (_locks)
                    _locks.Add(randomName, new());
            }
            await _settings.SetItem("CacheList", _cacheList);
            return randomName;
        }

        public Task<string> CreateCacheFileAsync() =>
            this.CreateCacheFileAsync(new TimeSpan(0, 10, 0));

        public Task DeleteCacheFileAsync(string ID)
        {
            throw new NotImplementedException();
        }

        public Task ForceClearCacheAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<FileStream> GetCacheFileStreamAsync(string ID)
        {
            await InitializeTask;

            if (await ContainsAsync(ID))
            {
                try
                {
                    lock (_locks)
                    {
                        _locks.TryGetValue(ID, out var locker);
                        locker!.EnterWriteLock();
                    } // ContainsAsync() 已经确保不为null
                    return await _storage.GetFileStreamAsync(_cachePath, ID);
                }
                catch (Exception)
                {
                    _cacheList.RemoveAll(x => x.FileName == ID);
                    throw new FileNotFoundException($"Can't open Cache File {ID}");
                }
            }
            else
            {
                throw new FileNotFoundException($"Cache File ID {ID} Not Found");
            }
        }

        public async Task UpdateCacheFileAsync(string ID)
        {
            await Task.Run(() =>
            {
                lock (_cacheList)
                {
                    var cache = _cacheList.Find(x => x.FileName == ID);
                    if (cache is not null)
                    {
                        cache.UpdatedTime = DateTime.Now;
                    }
                }
                _ = _settings.SetItem("CacheList", _cacheList);
            });
        }

        public async Task ReleaseCacheFile(string ID)
        {
            lock (_locks)
            {
                _locks.TryGetValue(ID, out var locker);
                locker!.ExitWriteLock();
            }
        }
    }
}
