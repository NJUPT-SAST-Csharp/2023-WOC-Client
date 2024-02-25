using SastWiki.Core.Models.Dto;

namespace SastWiki.Core.Contracts.Backend.Category;

public interface ICategoryProvider
{
    public Task<List<EntryDto>> GetCategoryByFullNameAsync(string fullName);

    public Task<List<string>> GetAllCategoryList();
}
