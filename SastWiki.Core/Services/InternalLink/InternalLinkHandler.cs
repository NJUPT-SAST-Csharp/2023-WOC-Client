using SastWiki.Core.Contracts.InternalLink;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SastWiki.Core.Services.InternalLink
{
    public class InternalLinkHandler(
        IInternalLinkService internalLinkService,
        IInternalLinkValidator internalLinkValidator
    ) : IInternalLinkHandler
    {
        public void Trigger(Uri ilink)
        {
            if (internalLinkValidator.Validate(ilink))
            {
                internalLinkService.Paths[ilink.AbsolutePath].Invoke(
                    this,
                    HttpUtility.ParseQueryString(ilink.Query)
                );
            }
        }
    }
}
