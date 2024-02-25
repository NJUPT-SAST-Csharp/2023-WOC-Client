namespace SastWiki.Core.Contracts.Infrastructure.CacheService;

public interface ICache<TCache>
{
    public Task AddAsync(string key, TCache value);

    public Task<bool> RemoveAsync(string key);

    public Task<TCache?> GetAsync(string key);

    public Task<bool> ContainsAsync(string key);
}
