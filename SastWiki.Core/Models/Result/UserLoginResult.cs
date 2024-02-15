using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SastWiki.Core.Models.Result
{
    public record UserLoginResult
    {
        public required bool IsAuthenticated { get; init; } = false;
        public required string Username { get; init; }
        public string? Role { get; init; }
        internal string? Token { get; init; }
        public string Message { get; init; } = string.Empty;
        public DateTime? LoginTime { get; init; }
        public DateTime? LastLoginTime { get; init; }
    }
}
