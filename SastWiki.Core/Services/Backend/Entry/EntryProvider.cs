using CommunityToolkit.Mvvm.Messaging;
using SastWiki.Core.Contracts.Backend;
using SastWiki.Core.Contracts.Backend.Entry;
using SastWiki.Core.Models.Dto;
using SastWiki.Core.Models.Messages;

namespace SastWiki.Core.Services.Backend.Entry;

public class EntryProvider(ISastWikiAPI _api, IEntryCache _cache) : IEntryProvider
{
    private readonly List<int> _entryIdList = [];

    public async Task DeleteEntryAsync(int id)
    {
        Task<Refit.IApiResponse<string>> deleteTask = _api.DeleteEntry(
            (await GetEntryMetadataList()).Where(x => x.Id == id).Select(x => x.Title).First()
                ?? string.Empty
        );

        if ((await deleteTask).IsSuccessStatusCode)
        {
            _cache.EntryMetadataList = null; // 清空词条列表缓存
            _ = GetEntryMetadataList();
        }
        else
        {
            throw (await deleteTask).Error!;
        }
    }

    public async Task<EntryDto> AddEntryAsync(EntryDto entry)
    {
        entry.Id = null;
        Task<Refit.IApiResponse<EntryDto>> postTask = _api.PostEntry(entry);

        Refit.IApiResponse<EntryDto> postResponse = await postTask;
        if (postResponse.IsSuccessStatusCode)
        {
            _cache.EntryMetadataList = null; // 清空词条列表缓存
            _ = GetEntryMetadataList();
            if (postResponse.Content is not null && postResponse.Content.Id is int id)
            {
                entry.Id = id;
                return entry;
            }
            else
            {
                throw new Exception($"Failed to add a entry. Title is {entry.Title ?? "[null]"}");
            }
        }
        else
        {
            throw postResponse.Error!; // ApiException.Content == "The entry has already exist!" 即标题重复
        }
    }

    public async Task<EntryDto> GetEntryByIdAsync(int id)
    {
        Task<Refit.IApiResponse<EntryDto>> getTask = _api.GetEntryById(id);
        _ = getTask.ContinueWith(
            async (task) =>
            {
                Refit.IApiResponse<EntryDto> entryResponse = await task;
                if (entryResponse.IsSuccessStatusCode && entryResponse.Content is not null)
                {
                    await _cache.AddAsync(id.ToString(), entryResponse.Content);
                }
            }
        );

        if (
            await _cache.ContainsAsync(id.ToString())
            && (await GetEntryMetadataList()).Where(x => x.Id == id).Any()
        )
        {
            try
            {
                EntryDto? cachedVer = await _cache.GetAsync(id.ToString());
                if (cachedVer is not null)
                {
                    return cachedVer;
                }
            }
            catch (Exception) { }
        }

        Refit.IApiResponse<EntryDto> entryResponse = await getTask;
        return entryResponse.IsSuccessStatusCode && entryResponse.Content is not null
            ? entryResponse.Content
            : throw entryResponse.Error!;
    }

    public async Task<bool> IsEntryExistsAsync(int id)
    {
        Task<List<EntryDto>> a = GetEntryMetadataList();
        return (await a).Select(entry => entry.Id).Contains(id);
    }

    public async Task<EntryDto> UpdateEntryAsync(EntryDto entry)
    {
        Task<Refit.IApiResponse<EntryDto>> postTask = _api.UpdateEntry(entry);

        if (!(await postTask).IsSuccessStatusCode)
        {
            throw (await postTask).Error!;
        }

        _cache.EntryMetadataList = null; // 清空词条列表缓存
        _ = GetEntryMetadataList();
        return (await postTask).Content;
    }

    public async Task<List<EntryDto>> GetEntryMetadataList()
    {
        if (_cache.EntryMetadataList is not null)
        {
            return _cache.EntryMetadataList;
        }

        // 从api获取词条列表
        Refit.IApiResponse<List<EntryDto>> entriesRequest = await _api.GetEntries();

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
            _ = WeakReferenceMessenger.Default.Send(new EntryMetadataChangedMessage(metadataList));
            return metadataList;
        }
        else
        {
            throw entriesRequest.Error!;
        }
    }
}
