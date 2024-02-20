using Refit;
using SastWiki.Core.Models.Dto;

namespace SastWiki.Core.Contracts.Backend
{
    public interface ISastWikiAPI
    {
        [Get("/api/Entry/GetEntries")]
        public Task<IApiResponse<List<EntryDto>>> GetEntries();

        [Get("/api/Entry/GetEntryById/{id}")]
        public Task<IApiResponse<EntryDto>> GetEntryById(int id);

        [Get("/api/Entry/GetEntryByTitle/{title}")]
        public Task<IApiResponse<EntryDto>> GetEntryByTitle(string title);

        [Get("/api/Entry/GetEntryByTags")]
        public Task<IApiResponse<List<EntryDto>>> GetEntryByTags(
            [Query(CollectionFormat.Multi)] List<string> tagNames
        );

        [Get("/api/Entry/GetEntryByCategory/{categoryName}")]
        public Task<IApiResponse<List<EntryDto>>> GetEntryByCategory(string categoryName);

        [Post("/api/Entry/PostEntry")]
        public Task<IApiResponse<EntryDto>> PostEntry(
            [Refit.Body(BodySerializationMethod.UrlEncoded)] EntryDto entry
        );
    }
}
