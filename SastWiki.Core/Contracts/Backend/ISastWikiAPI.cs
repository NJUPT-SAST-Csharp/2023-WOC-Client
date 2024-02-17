using Refit;
using SastWiki.Core.Models.Dto;

namespace SastWiki.Core.Contracts.Backend
{
    public interface ISastWikiAPI
    {
        [Get("/api/Entry/GetEntries")]
        public Task<List<EntryDto>> GetEntries();

        [Get("/api/Entry/GetEntryById/{id}")]
        public Task<EntryDto> GetEntryById(int id);

        [Get("/api/Entry/GetEntryByTitle/{title}")]
        public Task<EntryDto> GetEntryByTitle(string title);

        [Get("/api/Entry/GetEntryByTags")]
        public Task<List<EntryDto>> GetEntryByTags(
            [Query(CollectionFormat.Multi)] List<string> tagNames
        );

        [Get("/api/Entry/GetEntryByCategory/{categoryName}")]
        public Task<List<EntryDto>> GetEntryByCategory(string categoryName);

        [Post("/api/Entry/PostEntry")]
        public Task<EntryDto> PostEntry([Body] EntryDto entry);
    }
}
