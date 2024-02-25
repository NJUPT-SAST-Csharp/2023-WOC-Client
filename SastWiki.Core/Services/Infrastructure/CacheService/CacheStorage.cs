using Microsoft.Extensions.Options;
using SastWiki.Core.Contracts.Infrastructure;
using SastWiki.Core.Contracts.Infrastructure.CacheService;
using SastWiki.Core.Contracts.Infrastructure.SettingsService;
using SastWiki.Core.Models;

namespace SastWiki.Core.Services.Infrastructure.CacheService;

public class CacheStorage : ICacheStorage
{
    public CacheStorage(
        ISettingsProvider settings,
        ILocalStorage storage,
        IOptions<AppOptions> options
    )
    {
        _settings = settings;
        _storage = storage;
        _options = options;
        _cachePath = _options.Value.CacheBasePath;
        InitializeTask = InitializeAsync();
    }

    public Task InitializeTask { get; set; }

    private readonly ILocalStorage _storage;
    private readonly ISettingsProvider _settings;
    private readonly IOptions<AppOptions> _options;
    private readonly string _cachePath;
    private List<CacheFile> _cacheList = [];
    private readonly Dictionary<string, SemaphoreSlim> _locks = [];

    private async Task InitializeAsync()
    {
        try
        {
            List<CacheFile> cachefilelist;
            try
            {
                cachefilelist = await _settings.GetItem<List<CacheFile>>("CacheList") ?? [];
            }
            catch (Exception)
            {
                cachefilelist = [];
                await _settings.SetItem("CacheList", cachefilelist);
            }

            if (cachefilelist is not null)
            {
                _cacheList = cachefilelist;
                lock (_locks)
                {
                    foreach (CacheFile cache in cachefilelist)
                    {
                        _locks.Add(cache.FileName, new(1, 1));
                    }
                }
            }
            else
            {
                _cacheList = [];
            }
        }
        catch (Exception e)
        {
            throw new Exception("Cache Storage Init Fail", e);
        }
    }

    public Task ClearExpiredCacheAsync() => throw new NotImplementedException();

    public async Task<bool> ContainsAsync(string ID)
    {
        await InitializeTask;
        return _cacheList.Exists(x => x.FileName == ID)
            && _locks.ContainsKey(ID)
            && await _storage.Contains(_cachePath, ID);
    }

    public async Task<string> CreateCacheFileAsync(TimeSpan expireTime)
    {
        await InitializeTask;

        // Create a random cache file
        string randomName = Guid.NewGuid().ToString();
        await _storage.CreateAsync(_cachePath, randomName);
        lock (_cacheList)
        {
            _cacheList.Add(
                new CacheFile()
                {
                    FileName = randomName,
                    ExpireTime = expireTime,
                    UpdatedTime = DateTime.Now
                }
            );
            lock (_locks)
                _locks.Add(randomName, new(1, 1));
        }

        await _settings.SetItem("CacheList", _cacheList);
        return randomName;
    }

    public Task<string> CreateCacheFileAsync() => CreateCacheFileAsync(new TimeSpan(0, 10, 0));

    public Task DeleteCacheFileAsync(string ID) => throw new NotImplementedException();

    public async Task ForceClearCacheAsync()
    {
        // 删除全部缓存文件
        await InitializeTask;

        lock (_cacheList)
        {
            _cacheList = [];
        }

        await _settings.SetItem("CacheList", new List<CacheFile>());

        // 递归删除_cachePath下全部文件
        foreach (string file in Directory.GetFiles(_cachePath))
        {
            File.Delete(file);
        }
    }

    public async Task<FileStream> GetCacheFileStreamAsync(string ID)
    {
        await InitializeTask;

        if (await ContainsAsync(ID))
        {
            try
            {
                return await _storage.GetFileStreamAsync(_cachePath, ID);
            }
            catch (Exception)
            {
                _ = _cacheList.RemoveAll(x => x.FileName == ID);
                throw new FileNotFoundException($"Can't open Cache File {ID}");
            }
        }
        else
        {
            throw new FileNotFoundException($"Cache File ID {ID} Not Found");
        }
    }

    public async Task UpdateCacheFileAsync(string ID) =>
        await Task.Run(() =>
        {
            lock (_cacheList)
            {
                CacheFile? cache = _cacheList.Find(x => x.FileName == ID);
                if (cache is not null)
                {
                    cache.UpdatedTime = DateTime.Now;
                }
            }

            _ = _settings.SetItem("CacheList", _cacheList);
        });

    public async Task LockCacheFile(string ID)
    {
        try
        {
            _ = _locks.TryGetValue(ID, out SemaphoreSlim? locker);
            await locker!.WaitAsync();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public void ReleaseCacheFile(string ID)
    {
        try
        {
            _ = _locks.TryGetValue(ID, out SemaphoreSlim? locker);
            _ = locker!.Release();
        }
        catch (Exception)
        {
            throw;
        }
    }
}
