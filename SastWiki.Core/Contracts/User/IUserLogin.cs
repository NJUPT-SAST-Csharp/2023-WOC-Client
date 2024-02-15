using SastWiki.Core.Models.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        /// <returns>包含用户名，用户状态，Token等登录信息的 <seealso cref="UserLoginResult"/>，
        /// 如果成功登录<c>UserLoginResult.IsAuthenticated</c>为True，
        /// 反之为False，且<c>UserLoginResult.Message</c>中会包含错误信息。</returns>
        public Task<UserLoginResult> LoginAsync(string Username, string PasswordHash);

        /// <summary>
        /// 登出，即清除本地Token
        /// </summary>
        /// <returns><seealso cref="UserLoginResult"/>，不出意外的话<c>UserLoginResult.IsAuthenticated</c>为False</returns>
        public Task<UserLoginResult> LogoutAsync();
    }
}
