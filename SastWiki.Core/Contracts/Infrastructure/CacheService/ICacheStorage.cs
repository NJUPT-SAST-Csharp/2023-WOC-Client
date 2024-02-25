namespace SastWiki.Core.Contracts.Infrastructure.CacheService;

/// <summary>
/// 管理缓存文件
/// </summary>
public interface ICacheStorage
{
    public Task InitializeTask { get; set; }

    /// <summary>
    /// 删除所有缓存文件
    /// </summary>
    /// <returns></returns>
    public Task ForceClearCacheAsync();

    /// <summary>
    /// 删除过期的缓存文件
    /// </summary>
    /// <returns></returns>
    public Task ClearExpiredCacheAsync();

    /// <summary>
    /// 获取指定缓存文件的<seealso cref="FileStream"/>，如果没有指定文件就新建一个
    /// </summary>
    /// <param name="ID">指定的缓存文件的ID</param>
    /// <returns></returns>
    public Task<FileStream> GetCacheFileStreamAsync(string ID);

    /// <summary>
    /// 创建一个会在一定时间后自动被清除的缓存文件
    /// </summary>
    /// <param name="ExpireTime">这个缓存文件的过期时间，在执行<seealso cref="ClearExpiredCacheAsync"/>的时候会删除过期的缓存</param>
    /// <returns>返回该缓存文件的ID，可使用<seealso cref="GetCacheFileStreamAsync"/>获取该文件的<seealso cref="FileStream"/></returns>
    public Task<string> CreateCacheFileAsync(TimeSpan ExpireTime);

    /// <summary>
    /// 创建一个缓存文件
    /// </summary>
    /// <returns>返回该缓存文件的ID，可使用<seealso cref="GetCacheFileStreamAsync"/>获取该文件的<seealso cref="FileStream"/></returns>
    public Task<string> CreateCacheFileAsync();

    /// <summary>
    /// 删除指定的缓存文件
    /// </summary>
    /// <param name="ID">指定的缓存文件的ID</param>
    /// <returns></returns>
    public Task DeleteCacheFileAsync(string ID);

    /// <summary>
    /// 刷新指定的缓存文件的更新时间
    /// </summary>
    /// <param name="ID">指定的缓存文件的ID</param>
    /// <returns></returns>
    public Task UpdateCacheFileAsync(string ID);

    public Task<bool> ContainsAsync(string ID);

    public void ReleaseCacheFile(string ID);

    public Task LockCacheFile(string ID);
}
