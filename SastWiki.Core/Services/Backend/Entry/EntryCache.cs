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
    public class EntryCache(ICacheStorage _storage, ISettingsProvider _settings) : IEntryCache
    {
        TimeSpan _expireTime = new TimeSpan(0, 10, 0);
        Dictionary<string, string> _cahceFileID = [];

        public async Task AddAsync(string key, EntryDto value)
        {
            try
            {
                var createTask = _storage.CreateCacheFileAsync(_expireTime);
                var cacheFileStream = await _storage.GetCacheFileStreamAsync(await createTask);

                var writer = new StreamWriter(cacheFileStream);
                await writer.WriteAsync(JsonSerializer.Serialize(value));

                _cahceFileID.Add(key, await createTask);

                await writer.DisposeAsync();
            }
            catch (Exception e)
            {
                throw new Exception("Failed to add cache", e);
            }
        }

        public async Task<bool> ContainsAsync(string key)
        {
            return await Task.Run(() => _cahceFileID.TryGetValue(key, out _));
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
