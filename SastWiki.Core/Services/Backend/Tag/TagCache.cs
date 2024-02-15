using SastWiki.Core.Contracts.Backend.Tag;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SastWiki.Core.Services.Backend.Tag
{
    public class TagCache : ITagCache
    {
        public TagCache() { }

        public Task AddAsync(string key, string value)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ContainsAsync(string key)
        {
            throw new NotImplementedException();
        }

        public Task<string?> GetAsync(string key)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveAsync(string key)
        {
            throw new NotImplementedException();
        }
    }
}
