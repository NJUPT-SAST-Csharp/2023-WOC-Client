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

        public List<EntryDto>? EntryMetadataList { get; set; }

        ICacheStorage _storage;
        ISettingsProvider _settings;

        public EntryCache(ICacheStorage storage, ISettingsProvider settings)
        {
            _storage = storage;
            _settings = settings;
            try
            {
                _cahceFileID =
                    _settings.GetItem<Dictionary<string, string>>("EntryCacheList").Result ?? [];
            }
            catch (Exception)
            {
                // 如果 没有缓存文件与ID的映射关系 或 读取失败 则初始化并保存到设置文件中
                _cahceFileID = [];
                _settings.SetItem("EntryCacheList", _cahceFileID);
            }
        }

        public async Task AddAsync(string key, EntryDto value)
        {
            try
            {
                if (
                    _cahceFileID.ContainsKey(key) && await _storage.ContainsAsync(_cahceFileID[key])
                ) // 如果已经有了对应的缓存文件……
                {
                    using var cacheFileStream = await _storage.GetCacheFileStreamAsync(
                        _cahceFileID[key]
                    );
                    using var writer = new StreamWriter(cacheFileStream);
                    await writer.WriteAsync(JsonSerializer.Serialize(value)); // 就直接覆盖写入
                    await _storage.UpdateCacheFileAsync(_cahceFileID[key]); // 并且刷新缓存文件的更新时间
                }
                else
                {
                    var ID = await _storage.CreateCacheFileAsync(_expireTime); // 否则创建一个新的缓存文件

                    using var cacheFileStream = await _storage.GetCacheFileStreamAsync(ID);
                    using var writer = new StreamWriter(cacheFileStream);
                    await writer.WriteAsync(JsonSerializer.Serialize(value)); //写入缓存数据

                    _cahceFileID.Add(key, ID);
                    await _settings.SetItem("EntryCacheList", _cahceFileID); // 并且将新的缓存文件的ID与Key的映射关系保存到设置文件中
                }
            }
            catch (Exception e)
            {
                throw new Exception("Failed to add cache", e);
            }
        }

        public async Task<bool> ContainsAsync(string key)
        {
            return await Task.Run(() => _cahceFileID.ContainsKey(key));
        }

        public async Task<EntryDto?> GetAsync(string key)
        {
            if (await ContainsAsync(key))
            {
                return JsonSerializer.Deserialize<EntryDto>(
                    await (
                        new StreamReader(await _storage.GetCacheFileStreamAsync(_cahceFileID[key]))
                    ).ReadToEndAsync()
                );
            }
            else
            {
                return null;
            }
        }

        public Task<bool> RemoveAsync(string key)
        {
            throw new NotImplementedException();
        }
    }
}
