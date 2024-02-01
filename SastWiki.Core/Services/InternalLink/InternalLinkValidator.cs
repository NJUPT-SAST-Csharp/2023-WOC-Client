using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SastWiki.Core.Contracts.InternalLink;

namespace SastWiki.Core.Services.InternalLink
{
    public class InternalLinkValidator(IInternalLinkService internallinkservice)
        : IInternalLinkValidator
    {
        public string SchemeName { get; init; } = "wiki";
        public string HostName { get; init; } = "sast-wiki";

        public bool Validate(Uri uri) =>
            uri.Scheme == SchemeName
            && uri.Host == HostName
            && internallinkservice.ContainsPath(uri.AbsolutePath);
    }
}
