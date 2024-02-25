﻿using SastWiki.Core.Models.Dto;

namespace SastWiki.Core.Contracts.User;

/// <summary>
/// 获取用户登录状态的接口
/// </summary>
public interface IUserStatus
{
    public Task<UserDto> GetUserStatus();
    public Task<bool> IsUserLoggedin();
}
