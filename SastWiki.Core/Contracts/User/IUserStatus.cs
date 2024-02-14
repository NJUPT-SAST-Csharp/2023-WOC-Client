using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SastWiki.Core.Contracts.User
{
    /// <summary>
    /// 供前端获取用户登录状态的接口
    /// </summary>
    public interface IUserStatus
    {
        public bool IsLoggedIn { get; }
        public string? Username { get; }
        public DateTime? LoginTime { get; }
        public DateTime? LoginExpirationTime { get; }
        public string? Role { get; }
    }
}
