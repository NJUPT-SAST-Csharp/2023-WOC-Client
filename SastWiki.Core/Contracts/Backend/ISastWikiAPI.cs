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

        [Headers("Authorization: Bearer")]
        [Post("/api/Entry/PostEntry")]
        public Task<IApiResponse<EntryDto>> PostEntry(
            [Refit.Body(BodySerializationMethod.Serialized)] EntryDto entry
        );

        [Headers("Authorization: Bearer")]
        [Put("/api/Entry/UpdateEntry")]
        public Task<IApiResponse<EntryDto>> UpdateEntry(
            [Refit.Body(BodySerializationMethod.Serialized)] EntryDto entry
        );

        [Headers("Authorization: Bearer")]
        [Delete("/api/Entry/DeleteEntry")]
        public Task<IApiResponse<string>> DeleteEntry([Refit.Query] string title);

        // Picture
        [Multipart]
        [Headers("Authorization: Bearer")]
        [Post("/api/Picture/UploadPicture")]
        public Task<IApiResponse<PictureDto>> UploadPicture(ByteArrayPart pic);

        [Get("/api/Picture/GetPictureById")]
        public Task<IApiResponse<byte[]>> GetPictureById([Refit.Query] int id);

        // Role

        [Post("/api/Role/ApplyAdmin")]
        public Task<IApiResponse<string>> ApplyAdmin();

        [Headers("Authorization: Bearer")]
        [Post("/api/Role/SetAdmin")]
        public Task<IApiResponse<string>> SetAdmin([Refit.Query] string targetUserToken);

        // User

        [Post("/api/User/Login")]
        public Task<IApiResponse<string>> Login(
            [Refit.Query] string email,
            [Refit.Query] string password
        );

        [Put("/api/User/Signup")]
        public Task<IApiResponse<string>> Signup(
            [Refit.Query] string name,
            [Refit.Query] string email,
            [Refit.Query] string password
        );

        [Headers("Authorization: Bearer")]
        [Post("/api/User/Quit")]
        public Task<IApiResponse<string>> Quit();
    }
}
