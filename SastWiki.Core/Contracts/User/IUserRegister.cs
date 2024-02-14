using SastWiki.Core.Models.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SastWiki.Core.Contracts.User
{
    /// <summary>
    /// 提供用户注册的接口
    /// </summary>
    public interface IUserRegister
    {
        /// <summary>
        /// 使用用户名与密码（哈希过的密码）注册
        /// </summary>
        /// <param name="Username">用户名</param>
        /// <param name="PasswordHash">密码</param>
        /// <returns>包含用户名，用户状态，Token等登录信息的 <seealso cref="UserRegisterResult"/>，
        /// 如果成功登录<c>UserRegisterResult.IsRegistered</c>为True，
        /// 反之为False，且<c>UserRegisterResult.Message</c>中会包含错误信息。</returns>
        public Task<UserRegisterResult> Register(string Username, string PasswordHash);
    }
}
