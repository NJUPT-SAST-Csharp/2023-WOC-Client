﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using SastWiki.Core.Contracts.Infrastructure;
using SastWiki.Core.Contracts.Infrastructure.SettingsService;

namespace SastWiki.Core.Services.Infrastructure.SettingsService
{
    public class SettingsProvider : ISettingsProvider
    {
        ISettingsStorage _storage;

        public SettingsProvider(ISettingsStorage storage)
        {
            _storage = storage;
            InitializeTask = LoadAsync();
        }

        public Task InitializeTask;

        public async Task InitializeAsync()
        {
            await LoadAsync();
        }

        Dictionary<string, string> _settings = [];

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

                var result = await Task.Run(() => JsonSerializer.Deserialize<T>(categorySettings));
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
            // await Save();
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
                    await Save();
                }

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
                _settings = [];
                await Save();
                throw new Exception("Error loading settings", e);
            }
        }
    }
}
