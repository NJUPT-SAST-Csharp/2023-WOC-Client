using SastWiki.Core.Models.Dto;

namespace SastWiki.Core.Contracts.User;

/// <summary>
/// 存储凭据，借助ISettingsProvider存储在本地以供未来使用
/// </summary>
public interface IAuthenticationStorage
{
    internal UserDto CurrentUser { get; set; }
}
