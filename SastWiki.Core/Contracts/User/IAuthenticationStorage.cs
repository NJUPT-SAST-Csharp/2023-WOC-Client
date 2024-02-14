using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SastWiki.Core.Contracts.User
{
    /// <summary>
    /// 存储凭据，借助ISettingsProvider存储在本地以供未来使用
    /// </summary>
    internal interface IAuthenticationStorage
    {
        /// <summary>
        /// 用户登录的Token，若未登录则为null
        /// </summary>
        internal string? AuthenticationToken { get; set; }

        internal string? Username { get; set; }
        internal string? PasswordHash { get; set; }
    }
}
