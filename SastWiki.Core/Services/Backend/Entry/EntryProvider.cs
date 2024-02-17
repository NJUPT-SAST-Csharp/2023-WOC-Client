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
                return (await postTask).Id;
            }
            catch (Exception)
            {
                return -1;
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

        public async Task<List<int>> GetEntryIDListAsync()
        {
            var getTask = _api.GetEntries();
            _ = getTask.ContinueWith(
                async (task) =>
                {
                    var id = (await task).Select((EntryDto) => EntryDto.Id).ToList();
                    lock (_entryIdList)
                    {
                        _entryIdList = id;
                    }
                }
            );

            return (await getTask).Select((EntryDto) => EntryDto.Id).ToList();
        }
    }
}
