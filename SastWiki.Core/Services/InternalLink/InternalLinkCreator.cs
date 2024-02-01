using SastWiki.Core.Contracts.InternalLink;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SastWiki.Core.Services.InternalLink
{
    public class InternalLinkCreator(IInternalLinkValidator validator, IInternalLinkService service)
        : IInternalLinkCreator
    {
        public const string _schemeName = "wiki";
        public const string _hostName = "sast-wiki";

        public Uri Create(string path, Dictionary<string, string> query)
        // NameValueCollection的性能似乎不如Dictionary<string,string>？
        {
            if (!service.ContainsPath(path))
                throw new ArgumentException("path is not registered");
            StringBuilder sb = new StringBuilder();
            sb.Append($"{validator.SchemeName}://{validator.HostName}{path}");
            if (query.Count > 0)
            {
                sb.Append("?");
                foreach (KeyValuePair<string, string> kvp in query)
                {
                    sb.Append($"{kvp.Key}={kvp.Value}&");
                }
                sb.Remove(sb.Length - 1, 1);
            }
            return new Uri(sb.ToString());
        }
    }
}
