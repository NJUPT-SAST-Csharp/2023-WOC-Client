using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SastWiki.Core.Models.Dto;

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
        public Task RegisterAsync(string Username, string email, string PasswordHash);
    }
}
