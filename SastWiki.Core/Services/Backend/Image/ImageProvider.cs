using Refit;
using SastWiki.Core.Contracts.Backend;
using SastWiki.Core.Contracts.Backend.Image;
using SastWiki.Core.Models.Dto;

namespace SastWiki.Core.Services.Backend.Image;

public class ImageProvider(ISastWikiAPI api) : IImageProvider
{
    public async Task<PictureDto?> UploadImageAsync(byte[] image)
    {
        var streamPart = new ByteArrayPart(image, "filename.jpg", "image/jpeg");

        IApiResponse<PictureDto> uploadResponse = await api.UploadPicture(streamPart);
        return !uploadResponse.IsSuccessStatusCode
            ? throw uploadResponse.Error ?? new Exception("Image Upload Failed")
            : uploadResponse.Content;
    }

    public async Task<byte[]> GetImageAsync(int id)
    {
        IApiResponse<byte[]> imageResponse = await api.GetPictureById(id);
        return !imageResponse.IsSuccessStatusCode || imageResponse.Content is null
            ? throw imageResponse.Error ?? new Exception("Image Get Failed")
            : imageResponse.Content;
    }
}
