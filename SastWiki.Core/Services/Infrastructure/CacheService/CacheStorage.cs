using SastWiki.Core.Contracts.Infrastructure.CacheService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SastWiki.Core.Services.Infrastructure.CacheService
{
    public class CacheStorage : ICacheStorage
    {
        public CacheStorage() { }

        public Task ClearExpiredCacheAsync()
        {
            throw new NotImplementedException();
        }

        public Task<string> CreateCacheFileAsync(TimeSpan ExpireTime)
        {
            throw new NotImplementedException();
        }

        public Task<string> CreateCacheFileAsync()
        {
            throw new NotImplementedException();
        }

        public Task DeleteCacheFileAsync(string ID)
        {
            throw new NotImplementedException();
        }

        public Task ForceClearCacheAsync()
        {
            throw new NotImplementedException();
        }

        public Task<FileStream> GetCacheFileStreamAsync(string ID)
        {
            throw new NotImplementedException();
        }

        public Task UpdateCacheFileAsync(string ID)
        {
            throw new NotImplementedException();
        }
    }
}
