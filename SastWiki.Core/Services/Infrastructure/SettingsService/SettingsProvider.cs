using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using SastWiki.Core.Contracts.Infrastructure;
using SastWiki.Core.Contracts.Infrastructure.SettingsService;

namespace SastWiki.Core.Services.Infrastructure.SettingsService
{
    public class SettingsProvider(ISettingsStorage _storage) : ISettingsProvider
    {
        Dictionary<string, string> _settings = [];

        public async Task<T?> GetItem<T>(string label)
        {
            string? categorySettings;

            lock (_settings)
            {
                if (!_settings.TryGetValue(label, out categorySettings))
                    throw new ArgumentNullException(nameof(T));
                if (categorySettings is null)
                    throw new ArgumentNullException(nameof(T));
            }

            try
            {
                var result = await Task.Run(() => JsonSerializer.Deserialize<T>(categorySettings));
                return result;
            }
            catch (Exception e)
            {
                throw new Exception("Error getting settings", e);
            }
        }

        public async Task SetItem<T>(string label, T item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            var json = await Task.Run(() => JsonSerializer.Serialize<T>(item));

            lock (_settings)
            {
                if (!_settings.TryGetValue(label, out var categorySettings))
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
        }

        public async Task Save()
        {
            await _storage.SetSettingsJSON(JsonSerializer.Serialize(_settings));
        }

        public async Task Load()
        {
            try
            {
                var settingsJsonList = JsonSerializer.Deserialize<Dictionary<string, string>>(
                    await _storage.GetSettingsJSON()
                );

                if (settingsJsonList is not null)
                {
                    _settings = settingsJsonList;
                }
                else
                {
                    throw new ArgumentNullException(nameof(settingsJsonList));
                }
            }
            catch (Exception e)
            {
                throw new Exception("Error loading settings", e);
            }
        }
    }
}
