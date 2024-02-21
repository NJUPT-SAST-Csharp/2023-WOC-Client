using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SastWiki.Core.Models.Dto
{
    public class UserDto
    {
        public string? Email { get; set; }
        public string? Username { get; set; }
        public string? PasswordHash { get; set; }
        public string? Token { get; set; }
        public string? Role { get; set; }

        public override string ToString() =>
            $"UserDto {{ Email: {Email}, Username: {Username}, PasswordHash: {PasswordHash}, Token: {Token}, Role: {Role} }}";
    }
}
