using SastWiki.Core.Contracts.Backend;
using SastWiki.Core.Contracts.Backend.Entry;
using SastWiki.Core.Contracts.Backend.Tag;
using SastWiki.Core.Models.Dto;

namespace SastWiki.Core.Services.Backend.Tag;

public class TagProvider(IEntryProvider entryProvider, ISastWikiAPI api) : ITagProvider
{
    public async Task<List<string>> GetAllTagsList() =>
        [.. (await entryProvider.GetEntryMetadataList()).SelectMany(x => x.TagNames).Distinct()];

    public async Task<List<EntryDto>> GetEntryByTags(List<string> tags) =>
        [
            .. (await entryProvider.GetEntryMetadataList()).Where(x =>
                x.TagNames.Distinct().Intersect(tags).Count() == tags.Count
            )
        ];
}
