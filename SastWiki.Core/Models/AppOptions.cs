namespace SastWiki.Core.Models;

public class AppOptions
{
    public string AppName { get; set; } = "SastWiki";

    public string AppVersion { get; set; } = "0.0.1";

    public string CacheBasePath { get; set; } = "D:\\SastWiki\\Cache";

    public string SettingsFilePath { get; set; } = "D:\\SastWiki";

    public string ServerURI { get; set; } = "http://localhost:5281/";

    public string HostName { get; set; } = "sast-wiki";
}
