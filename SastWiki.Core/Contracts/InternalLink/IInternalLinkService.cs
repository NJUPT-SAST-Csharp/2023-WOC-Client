using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SastWiki.Core.Contracts.InternalLink
{
    public interface IInternalLinkService
    {
        public Dictionary<string, EventHandler<NameValueCollection>> Paths { get; }

        public bool Register(string path, EventHandler<NameValueCollection> handler);

        public bool ContainsPath(string path);
    }
}
