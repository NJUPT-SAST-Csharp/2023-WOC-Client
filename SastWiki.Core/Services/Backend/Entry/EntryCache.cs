using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using SastWiki.Core.Contracts.Backend.Entry;
using SastWiki.Core.Contracts.Infrastructure.CacheService;
using SastWiki.Core.Contracts.Infrastructure.SettingsService;
using SastWiki.Core.Models.Dto;

namespace SastWiki.Core.Services.Backend.Entry
{
    public class EntryCache : IEntryCache
    {
        readonly TimeSpan _expireTime = new TimeSpan(0, 10, 0);

        Dictionary<string, string> _cahceFileID = [];

        object _entryMetadataListlock = new object();
        private List<EntryDto>? _entryMetadataList;
        public List<EntryDto>? EntryMetadataList
        {
            get
            {
                lock (_entryMetadataListlock)
                {
                    return _entryMetadataList;
                }
            }
            set
            {
                lock (_entryMetadataListlock)
                {
                    _entryMetadataList = value;
                }
            }
        }

        ICacheStorage _storage;
        ISettingsProvider _settings;

        public EntryCache(ICacheStorage storage, ISettingsProvider settings)
        {
            _storage = storage;
            _settings = settings;
            InitializeTask = InitializeAsync();
        }

        public Task InitializeTask;

        public async Task InitializeAsync()
        {
            try
            {
                var task = _settings.GetItem<Dictionary<string, string>>("EntryCacheList");

                _cahceFileID = await task ?? [];
            }
            catch (Exception)
            {
                // 如果 没有缓存文件与ID的映射关系 或 读取失败 则初始化并保存到设置文件中
                _cahceFileID = [];
                await _settings.SetItem("EntryCacheList", _cahceFileID);
            }
        }

        public async Task AddAsync(string key, EntryDto value)
        {
            await InitializeTask;
            try
            {
                if (
                    _cahceFileID.ContainsKey(key) && await _storage.ContainsAsync(_cahceFileID[key])
                ) // 如果已经有了对应的缓存文件……
                {
                    await _storage.LockCacheFile(_cahceFileID[key]);
                    FileStream fileStream = await _storage.GetCacheFileStreamAsync(
                        _cahceFileID[key]
                    );
                    using var cacheFileStream = fileStream;
                    using var writer = new StreamWriter(cacheFileStream);
                    await writer.WriteAsync(JsonSerializer.Serialize(value)); // 就直接覆盖写入
                    await _storage.UpdateCacheFileAsync(_cahceFileID[key]); // 并且刷新缓存文件的更新时间
                }
                else
                {
                    var ID = await _storage.CreateCacheFileAsync(_expireTime); // 否则创建一个新的缓存文件

                    using var cacheFileStream = await _storage.GetCacheFileStreamAsync(ID);
                    using var writer = new StreamWriter(cacheFileStream);
                    if (_cahceFileID.ContainsKey(key))
                        _cahceFileID[key] = ID;
                    else
                        _cahceFileID.Add(key, ID);
                    await _storage.LockCacheFile(_cahceFileID[key]);
                    await writer.WriteAsync(JsonSerializer.Serialize(value)); //写入缓存数据

                    await _settings.SetItem("EntryCacheList", _cahceFileID); // 并且将新的缓存文件的ID与Key的映射关系保存到设置文件中
                }
            }
            catch (Exception e)
            {
                throw new Exception("Failed to add cache", e);
            }
            finally
            {
                _storage.ReleaseCacheFile(_cahceFileID[key]); // 释放缓存文件
            }
        }

        public async Task<bool> ContainsAsync(string key)
        {
            await InitializeTask;
            return await Task.Run(() => _cahceFileID.ContainsKey(key));
        }

        public async Task<EntryDto?> GetAsync(string key)
        {
            await InitializeTask;
            if (await ContainsAsync(key))
            {
                await _storage.LockCacheFile(_cahceFileID[key]);
                try
                {
                    using var fs = await _storage.GetCacheFileStreamAsync(_cahceFileID[key]);
                    var data = await (new StreamReader(fs)).ReadToEndAsync();
                    return JsonSerializer.Deserialize<EntryDto>(data);
                }
                catch (Exception)
                {
                    return null;
                }
                finally
                {
                    _storage.ReleaseCacheFile(_cahceFileID[key]);
                }
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> RemoveAsync(string key)
        {
            await InitializeTask;
            throw new NotImplementedException();
        }
    }
}
