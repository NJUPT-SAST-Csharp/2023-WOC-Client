using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SastWiki.Core.Contracts.Backend;
using SastWiki.Core.Contracts.Backend.Entry;
using SastWiki.Core.Models.Dto;

namespace SastWiki.Core.Services.Backend.Entry
{
    public class EntryProvider(ISastWikiAPI _api, IEntryCache _cache) : IEntryProvider
    {
        private List<int> _entryIdList = [];

        public async Task<int> AddEntryAsync(EntryDto entry)
        {
            entry.Id = -1;
            var postTask = _api.PostEntry(entry);
            try
            {
                _cache.EntryMetadataList = null; // 清空词条列表缓存
                return (await postTask).Id;
            }
            catch (Exception e)
            {
                throw new Exception(
                    $"Failed to add a entry. Title is {entry.Title ?? "[null]"}",
                    e
                );
            }
        }

        public async Task<EntryDto> GetEntryByIdAsync(int id)
        {
            var getTask = _api.GetEntryById(id);
            _ = getTask.ContinueWith(
                async (task) =>
                {
                    try
                    {
                        var entry = await task;
                        _ = _cache.AddAsync(id.ToString(), entry);
                    }
                    catch (Exception)
                    {
                        await _cache.RemoveAsync(id.ToString());
                    }
                }
            );

            if (await _cache.ContainsAsync(id.ToString()))
            {
                var cachedVer = await _cache.GetAsync(id.ToString());
                if (cachedVer is not null)
                {
                    return cachedVer as EntryDto;
                }
            }

            return await getTask;
        }

        public async Task<bool> IsEntryExistsAsync(int id)
        {
            var a = GetEntryIDListAsync();
            return (await a).Contains(id);
        }

        public async Task<bool> UpdateEntryAsync(EntryDto entry)
        {
            try
            {
                await _api.PostEntry(entry);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<EntryDto>> GetEntryMetadataList()
        {
            if (_cache.EntryMetadataList is not null)
            {
                return _cache.EntryMetadataList;
            }

            // 从api获取词条列表
            try
            {
                var entries = await _api.GetEntries();
                var metadataList = entries
                    .Select(entry =>
                    {
                        entry.Content = null;
                        return entry;
                    })
                    .ToList();
                _ = Task.Run(() =>
                {
                    _cache.EntryMetadataList = metadataList;
                });

                return metadataList;
            }
            catch (Exception e)
            {
                throw new Exception("Get Entry Metadata Error, and no cached ver available.", e);
            }
        }
    }
}
