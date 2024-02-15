using SastWiki.Core.Contracts.Backend.Tag;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SastWiki.Core.Services.Backend.Tag
{
    public class TagProvider : ITagProvider
    {
        public TagProvider() { }

        public Task<IEnumerable<string>> GetAllTagsList()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<int>> GetEntryIdListByTag(string tag)
        {
            throw new NotImplementedException();
        }
    }
}
