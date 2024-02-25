using SastWiki.Core.Contracts.Backend;
using SastWiki.Core.Contracts.Backend.Category;
using SastWiki.Core.Contracts.Backend.Entry;
using SastWiki.Core.Models.Dto;

namespace SastWiki.Core.Services.Backend.Category;

public class CategoryProvider(IEntryProvider entryProvider, ISastWikiAPI api) : ICategoryProvider
{
    public async Task<List<string>> GetAllCategoryList() =>
        [
            .. (await entryProvider.GetEntryMetadataList())
                .Select(x => x.CategoryName ?? string.Empty)
                .Distinct()
        ];

    public async Task<List<EntryDto>> GetCategoryByFullNameAsync(string fullName) =>
        [
            .. (await entryProvider.GetEntryMetadataList())
                .Where(x => x.CategoryName == fullName)
                .OrderBy(x => x.Id)
        ];
}
