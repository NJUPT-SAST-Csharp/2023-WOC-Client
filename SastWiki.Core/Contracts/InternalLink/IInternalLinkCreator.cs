namespace SastWiki.Core.Contracts.InternalLink;

public interface IInternalLinkCreator
{
    public Uri Create(string path, Dictionary<string, string> query);
}
