namespace SastWiki.Core.Models;

public class CacheFile
{
    public required string FileName { get; set; }
    public DateTime UpdatedTime { get; set; }
    public TimeSpan ExpireTime { get; set; }
}
