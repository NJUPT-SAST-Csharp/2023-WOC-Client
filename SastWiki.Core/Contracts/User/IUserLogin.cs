using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SastWiki.Core.Models.Dto;

namespace SastWiki.Core.Contracts.User
{
    /// <summary>
    /// 提供用户登录与登出的接口
    /// </summary>
    public interface IUserLogin
    {
        /// <summary>
        /// 使用用户名与密码（哈希过的密码）登录
        /// </summary>
        /// <param name="Username">用户名</param>
        /// <param name="PasswordHash">密码</param>
        public Task<UserDto> LoginAsync(string Username, string PasswordHash);

        /// <summary>
        /// 登出，即清除本地Token
        /// </summary>
        public Task<UserDto> LogoutAsync();
    }
}
