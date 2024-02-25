using System.Collections.Specialized;

namespace SastWiki.Core.Contracts.InternalLink;

public interface IInternalLinkService
{
    public Dictionary<string, EventHandler<NameValueCollection>> Paths { get; }

    public bool Register(string path, EventHandler<NameValueCollection> handler);

    public bool ContainsPath(string path);
}
