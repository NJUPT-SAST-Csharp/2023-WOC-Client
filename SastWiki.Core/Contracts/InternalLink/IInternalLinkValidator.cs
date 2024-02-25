namespace SastWiki.Core.Contracts.InternalLink;

public interface IInternalLinkValidator
{
    public string SchemeName { get; init; }
    public string HostName { get; init; }
    public bool Validate(Uri uri);
}
