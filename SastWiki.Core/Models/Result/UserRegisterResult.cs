using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SastWiki.Core.Models.Result
{
    public record UserRegisterResult
    {
        public required bool IsRegistered { get; init; } = false;
        public required string Username { get; init; }
        public string? Role { get; init; }
        internal string? Token { get; init; }
        public string Message { get; init; } = string.Empty;
        public DateTime? RegisterTime { get; init; }
    }
}
