namespace SastWiki.Core.Contracts.Infrastructure.SettingsService;

public interface ISettingsStorage
{
    /// <summary>
    /// 读取设置json文件的内容
    /// </summary>
    /// <returns></returns>
    public Task<string> GetSettingsJSON();

    /// <summary>
    /// 将json写入到设置文件里
    /// </summary>
    /// <param name="json"></param>
    /// <returns></returns>
    public Task SetSettingsJSON(string json);
}
