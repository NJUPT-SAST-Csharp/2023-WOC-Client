using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SastWiki.Core.Contracts.Infrastructure.CacheService;

namespace SastWiki.Core.Contracts.Backend.Image
{
    public interface IImageCache : ICache<byte[]> { }
}
