using SastWiki.Core.Models.Dto;

namespace SastWiki.Core.Contracts.Backend.Image;

public interface IImageProvider
{
    public Task<PictureDto> UploadImageAsync(byte[] image);

    public Task<byte[]> GetImageAsync(int id);
}
