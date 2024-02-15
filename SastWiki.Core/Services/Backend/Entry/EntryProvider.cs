using SastWiki.Core.Contracts.Backend.Entry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SastWiki.Core.Services.Backend.Entry
{
    public class EntryProvider : IEntryProvider
    {
        public EntryProvider() { }

        public Task<int> AddEntryAsync(Models.Entry entry)
        {
            throw new NotImplementedException();
        }

        public Task<Models.Entry> GetEntryByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsEntryExistsAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateEntryAsync(Models.Entry entry)
        {
            throw new NotImplementedException();
        }
    }
}
