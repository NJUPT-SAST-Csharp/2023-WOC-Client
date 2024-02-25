using System.Web;
using SastWiki.Core.Contracts.InternalLink;

namespace SastWiki.Core.Services.InternalLink;

public class InternalLinkHandler(
    IInternalLinkService internalLinkService,
    IInternalLinkValidator internalLinkValidator
) : IInternalLinkHandler
{
    public void Trigger(Uri ilink)
    {
        if (internalLinkValidator.Validate(ilink))
        {
            internalLinkService
                .Paths[ilink.AbsolutePath]
                .Invoke(this, HttpUtility.ParseQueryString(ilink.Query));
        }
    }
}
