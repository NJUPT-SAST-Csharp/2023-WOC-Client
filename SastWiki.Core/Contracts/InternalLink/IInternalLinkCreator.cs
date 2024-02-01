using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SastWiki.Core.Contracts.InternalLink
{
    public interface IInternalLinkCreator
    {
        public Uri Create(string path, Dictionary<string, string> query);
    }
}
