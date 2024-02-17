using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SastWiki.Core.Contracts.Infrastructure;
using SastWiki.Core.Contracts.Infrastructure.SettingsService;

namespace SastWiki.Core.Services.Infrastructure.SettingsService
{
    public class SettingsStorage(ILocalStorage _localStorage) : ISettingsStorage
    {
        string _settingsFilePath = "D:\\settings";
        string _settingsFileName = "settings.json";

        public async Task<string> GetSettingsJSON()
        {
            if (await _localStorage.Contains(_settingsFilePath, _settingsFileName))
            {
                try
                {
                    using var stream = await _localStorage.GetFileStreamAsync(
                        _settingsFilePath,
                        _settingsFileName
                    );
                    var json = await new StreamReader(stream).ReadToEndAsync();
                    return json;
                }
                catch (Exception e)
                {
                    throw new Exception("读取设置文件失败", e);
                }
            }
            else
            {
                throw new FileNotFoundException();
            }
        }

        public async Task SetSettingsJSON(string json)
        {
            if (json is null)
            {
                throw new ArgumentNullException(nameof(json));
            }

            if (await _localStorage.Contains(_settingsFilePath, _settingsFileName))
            {
                try
                {
                    using var stream = await _localStorage.GetFileStreamAsync(
                        _settingsFilePath,
                        _settingsFileName
                    );
                    await new StreamWriter(stream).WriteAsync(json);
                }
                catch (Exception e)
                {
                    throw new Exception("写入设置文件失败", e);
                }
            }
            else
            {
                throw new FileNotFoundException();
            }
        }
    }
}
