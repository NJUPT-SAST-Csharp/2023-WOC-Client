using Microsoft.Extensions.Options;
using SastWiki.Core.Contracts.Infrastructure;
using SastWiki.Core.Contracts.Infrastructure.SettingsService;
using SastWiki.Core.Models;

namespace SastWiki.Core.Services.Infrastructure.SettingsService;

public class SettingsStorage(ILocalStorage _localStorage, IOptions<AppOptions> _options)
    : ISettingsStorage
{
    private readonly string _settingsFilePath = _options.Value.SettingsFilePath;
    private readonly string _settingsFileName = "settings.json";
    private readonly object SettingsFileLock = new();

    public async Task<string> GetSettingsJSON()
    {
        if (!await _localStorage.Contains(_settingsFilePath, _settingsFileName))
            await _localStorage.CreateAsync(_settingsFilePath, _settingsFileName);

        try
        {
            lock (SettingsFileLock)
            {
                using FileStream stream = _localStorage.GetFileStream(
                    _settingsFilePath,
                    _settingsFileName
                );

                string json = new StreamReader(stream).ReadToEnd();
                return json;
            }
        }
        catch (Exception e)
        {
            throw new Exception("读取设置文件失败", e);
        }
    }

    public async Task SetSettingsJSON(string json)
    {
        if (json is null)
        {
            throw new ArgumentNullException(nameof(json));
        }

        if (!await _localStorage.Contains(_settingsFilePath, _settingsFileName))
            await _localStorage.CreateAsync(_settingsFilePath, _settingsFileName);

        try
        {
            lock (SettingsFileLock)
            {
                using FileStream stream = _localStorage.GetFileStream(
                    _settingsFilePath,
                    _settingsFileName
                );

                using var sw = new StreamWriter(stream);
                // discard the contents of the file by setting the length to 0
                stream.SetLength(0);

                // write the new content
                sw.Write(json);
            }
        }
        catch (Exception e)
        {
            throw new Exception("写入设置文件失败", e);
        }
    }
}
