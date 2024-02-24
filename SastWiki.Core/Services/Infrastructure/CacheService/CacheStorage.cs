using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using SastWiki.Core.Contracts.Infrastructure;
using SastWiki.Core.Contracts.Infrastructure.CacheService;
using SastWiki.Core.Contracts.Infrastructure.SettingsService;
using SastWiki.Core.Models;

namespace SastWiki.Core.Services.Infrastructure.CacheService
{
    public class CacheStorage : ICacheStorage
    {
        public CacheStorage(
            ISettingsProvider settings,
            ILocalStorage storage,
            IOptions<AppOptions> options
        )
        {
            _settings = settings;
            _storage = storage;
            _options = options;
            _cachePath = _options.Value.CacheBasePath;
            InitializeTask = InitializeAsync();
        }

        Task InitializeTask;
        readonly ILocalStorage _storage;
        readonly ISettingsProvider _settings;
        readonly IOptions<AppOptions> _options;

        readonly string _cachePath;
        List<CacheFile> _cacheList = [];
        Dictionary<string, SemaphoreSlim> _locks = [];

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
                            _locks.Add(cache.FileName, new(1, 1));
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
                    _locks.Add(randomName, new(1, 1));
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

        public async Task ForceClearCacheAsync()
        {
            // 删除全部缓存文件
            await InitializeTask;

            lock (_cacheList)
            {
                _cacheList = [];
            }

            await _settings.SetItem("CacheList", new List<CacheFile>());

            // 递归删除_cachePath下全部文件
            foreach (var file in Directory.GetFiles(_cachePath))
            {
                File.Delete(file);
            }
        }

        public async Task<FileStream> GetCacheFileStreamAsync(string ID)
        {
            await InitializeTask;

            if (await ContainsAsync(ID))
            {
                try
                {
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

        public async Task LockCacheFile(string ID)
        {
            try
            {
                _locks.TryGetValue(ID, out var locker);
                await locker!.WaitAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void ReleaseCacheFile(string ID)
        {
            try
            {
                _locks.TryGetValue(ID, out var locker);
                locker!.Release();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
