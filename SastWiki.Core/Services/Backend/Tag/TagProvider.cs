using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SastWiki.Core.Contracts.Backend;
using SastWiki.Core.Contracts.Backend.Entry;
using SastWiki.Core.Contracts.Backend.Tag;
using SastWiki.Core.Models.Dto;
using SastWiki.Core.Services.Backend.Entry;

namespace SastWiki.Core.Services.Backend.Tag
{
    public class TagProvider(IEntryProvider entryProvider, ISastWikiAPI api) : ITagProvider
    {
        public async Task<List<string>> GetAllTagsList() =>
            [
                .. (await entryProvider.GetEntryMetadataList())
                    .SelectMany(x => x.TagNames)
                    .Distinct()
            ];

        public async Task<List<EntryDto>> GetEntryByTags(List<string> tags) =>
            [
                .. (await entryProvider.GetEntryMetadataList()).Where(x =>
                    x.TagNames.Distinct().Intersect(tags).Count() == tags.Count
                )
            ];
    }
}
