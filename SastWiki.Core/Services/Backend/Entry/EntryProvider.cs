using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Refit;
using SastWiki.Core.Contracts.Backend;
using SastWiki.Core.Contracts.Backend.Entry;
using SastWiki.Core.Models.Dto;
using SastWiki.Core.Models.Exceptions;

namespace SastWiki.Core.Services.Backend.Entry
{
    public class EntryProvider(ISastWikiAPI _api, IEntryCache _cache) : IEntryProvider
    {
        private List<int> _entryIdList = [];

        public async Task<int> AddEntryAsync(EntryDto entry)
        {
            entry.Id = -1;
            var postTask = _api.PostEntry(entry);

            _cache.EntryMetadataList = null; // 清空词条列表缓存
            var postResponse = await postTask;
            if (postResponse.IsSuccessStatusCode)
            {
                if (postResponse.Content is not null)
                    return postResponse.Content.Id;
                else
                    throw new Exception(
                        $"Failed to add a entry. Title is {entry.Title ?? "[null]"}"
                    );
            }
            else
            {
                if (postResponse.StatusCode == HttpStatusCode.BadRequest)
                {
                    throw new NoPermissionException(
                        $"No permission to add the entry. Title is {entry.Title ?? "[null]"}"
                    );
                }
                else
                {
                    throw postResponse.Error
                        ?? new Exception(
                            $"Failed to add a entry. Title is {entry.Title ?? "[null]"}"
                        );
                }
            }
        }

        public async Task<EntryDto> GetEntryByIdAsync(int id)
        {
            var getTask = _api.GetEntryById(id);
            _ = getTask.ContinueWith(
                async (task) =>
                {
                    var entryResponse = await task;
                    if (entryResponse.IsSuccessStatusCode && entryResponse.Content is not null)
                    {
                        await _cache.AddAsync(id.ToString(), entryResponse.Content);
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

            var entryResponse = await getTask;
            if (entryResponse.IsSuccessStatusCode && entryResponse.Content is not null)
            {
                return entryResponse.Content;
            }
            else if (entryResponse.StatusCode == HttpStatusCode.NotFound)
            {
                throw new NotFoundException($"Entry not found. Id is {id}");
            }
            else
            {
                throw entryResponse.Error ?? new Exception($"Failed to get a entry. Id is {id}");
            }
        }

        public async Task<bool> IsEntryExistsAsync(int id)
        {
            var a = GetEntryMetadataList();
            return (await a).Select(entry => entry.Id).Contains(id);
        }

        public async Task UpdateEntryAsync(EntryDto entry)
        {
            var postTask = _api.PostEntry(entry);
            if (!(await postTask).IsSuccessStatusCode)
            {
                if ((await postTask).StatusCode == HttpStatusCode.BadRequest)
                {
                    throw new NoPermissionException(
                        $"No permission to update the entry. Id is {entry.Id}"
                    );
                }
                else if ((await postTask).StatusCode == HttpStatusCode.NotFound)
                {
                    throw new NotFoundException($"Entry not found. Id is {entry.Id}");
                }
                else
                {
                    throw (await postTask).Error
                        ?? new Exception($"Failed to update a entry. Id is {entry.Id}");
                }
            }
        }

        public async Task<List<EntryDto>> GetEntryMetadataList()
        {
            if (_cache.EntryMetadataList is not null)
            {
                return _cache.EntryMetadataList;
            }

            // 从api获取词条列表
            var entriesRequest = await _api.GetEntries();

            if (entriesRequest.IsSuccessStatusCode && entriesRequest.Content is not null)
            {
                var metadataList = entriesRequest
                    .Content.Select(entry =>
                    {
                        entry.Content = null;
                        return entry;
                    })
                    .ToList();

                await Task.Run(() =>
                {
                    _cache.EntryMetadataList = metadataList;
                });
                return metadataList;
            }
            else if (entriesRequest.StatusCode == HttpStatusCode.NotFound)
            {
                throw new NotFoundException("Entry not found.");
            }
            else
            {
                throw entriesRequest.Error
                    ?? new Exception("Failed to retrieve entry metadata list.");
            }
        }
    }
}
