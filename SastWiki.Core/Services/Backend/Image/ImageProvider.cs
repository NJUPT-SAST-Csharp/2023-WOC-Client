using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Refit;
using SastWiki.Core.Contracts.Backend;
using SastWiki.Core.Contracts.Backend.Image;
using SastWiki.Core.Models.Dto;

namespace SastWiki.Core.Services.Backend.Image
{
    public class ImageProvider(ISastWikiAPI api) : IImageProvider
    {
        public async Task<PictureDto> UploadImageAsync(byte[] image)
        {
            var streamPart = new ByteArrayPart(image, "filename.jpg", "image/jpeg");

            var uploadResponse = await api.UploadPicture(streamPart);
            if (!uploadResponse.IsSuccessStatusCode)
            {
                throw uploadResponse.Error ?? new Exception("Image Upload Failed");
            }
            return uploadResponse.Content;
        }

        public async Task<byte[]> GetImageAsync(int id)
        {
            var imageResponse = await api.GetPictureById(id);
            if (!imageResponse.IsSuccessStatusCode || imageResponse.Content is null)
            {
                throw imageResponse.Error ?? new Exception("Image Get Failed");
            }
            return imageResponse.Content;
        }
    }

    public class NonTimeoutMemoryStream : MemoryStream
    {
        public override bool CanTimeout => false;

        public NonTimeoutMemoryStream(byte[] buffer)
            : base(buffer) { }
    }
}
