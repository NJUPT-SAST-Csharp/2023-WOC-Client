using SastWiki.Core.Contracts.Backend.Image;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SastWiki.Core.Services.Backend.Image
{
    public class ImageCache : IImageCache
    {
        public ImageCache() { }

        public Task AddAsync(string key, byte[] value)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ContainsAsync(string key)
        {
            throw new NotImplementedException();
        }

        public Task<byte[]?> GetAsync(string key)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveAsync(string key)
        {
            throw new NotImplementedException();
        }
    }
}
