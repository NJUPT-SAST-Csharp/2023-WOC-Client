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
        TimeSpan _expireTime = new TimeSpan(0, 10, 0);
        Dictionary<string, string> _cahceFileID = [];
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
                _cahceFileID = [];
                _settings.SetItem("EntryCacheList", _cahceFileID);
            }
        }

        public async Task AddAsync(string key, EntryDto value)
        {
            try
            {
                if (_cahceFileID.ContainsKey(key))
                {
                    using var cacheFileStream = await _storage.GetCacheFileStreamAsync(
                        _cahceFileID[key]
                    );
                    using var writer = new StreamWriter(cacheFileStream);
                    await writer.WriteAsync(JsonSerializer.Serialize(value));
                    await _storage.UpdateCacheFileAsync(_cahceFileID[key]);
                }
                else
                {
                    var ID = await _storage.CreateCacheFileAsync(_expireTime);

                    using var cacheFileStream = await _storage.GetCacheFileStreamAsync(ID);
                    using var writer = new StreamWriter(cacheFileStream);
                    await writer.WriteAsync(JsonSerializer.Serialize(value));

                    _cahceFileID.Add(key, ID);
                    await _settings.SetItem("EntryCacheList", _cahceFileID);
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
