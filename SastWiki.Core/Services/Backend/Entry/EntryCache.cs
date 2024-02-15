using SastWiki.Core.Contracts.Backend.Entry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SastWiki.Core.Services.Backend.Entry
{
    public class EntryCache : IEntryCache
    {
        public EntryCache() { }

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
