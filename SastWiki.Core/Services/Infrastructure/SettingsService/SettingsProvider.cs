using System.Text.Json;
using SastWiki.Core.Contracts.Infrastructure.SettingsService;

namespace SastWiki.Core.Services.Infrastructure.SettingsService;

public class SettingsProvider : ISettingsProvider
{
    private readonly ISettingsStorage _storage;

    public SettingsProvider(ISettingsStorage storage)
    {
        _storage = storage;
        InitializeTask = LoadAsync();
    }

    public Task InitializeTask;

    public async Task InitializeAsync() => await LoadAsync();

    private Dictionary<string, string> _settings = [];

    public async Task<T?> GetItem<T>(string label)
    {
        await InitializeTask;
        string? categorySettings;
        try
        {
            lock (_settings)
            {
                if (!_settings.TryGetValue(label, out categorySettings))
                    throw new ArgumentNullException(nameof(T));
                if (categorySettings is null)
                    throw new ArgumentNullException(nameof(T));
            }

            T? result = await Task.Run(() => JsonSerializer.Deserialize<T>(categorySettings));
            return result;
        }
        catch (Exception e)
        {
            throw new Exception($"读取设置{label}失败", e);
        }
    }

    public async Task SetItem<T>(string label, T item)
    {
        await InitializeTask;
        if (item is null)
        {
            throw new ArgumentNullException(nameof(item));
        }

        string json = await Task.Run(() => JsonSerializer.Serialize<T>(item));

        lock (_settings)
        {
            if (!_settings.TryGetValue(label, out string? categorySettings))
            {
                // 不存在就新建
                _settings.Add(label, JsonSerializer.Serialize<T>(item));
            }
            else
            {
                // 存在就修改
                _settings[label] = JsonSerializer.Serialize<T>(item);
            }
        }

        await Save();
    }

    public async Task Save()
    {
        await InitializeTask;
        // await Console.Out.WriteLineAsync(JsonSerializer.Serialize(_settings));
        await _storage.SetSettingsJSON(JsonSerializer.Serialize(_settings));
    }

    public async Task LoadAsync()
    {
        try
        {
            Dictionary<string, string>? settingsJsonList;
            // await Console.Out.WriteLineAsync(await _storage.GetSettingsJSON());
            try
            {
                settingsJsonList = JsonSerializer.Deserialize<Dictionary<string, string>>(
                    await _storage.GetSettingsJSON()
                );
            }
            catch (Exception)
            {
                settingsJsonList = [];
                _settings = [];
                await _storage.SetSettingsJSON(JsonSerializer.Serialize(_settings));
            }

            _settings = settingsJsonList is not null
                ? settingsJsonList
                : throw new ArgumentNullException(nameof(settingsJsonList));
        }
        catch (Exception e)
        {
            _settings = [];
            await Save();
            throw new Exception("Error loading settings", e);
        }
    }
}
