using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SastWiki.Core.Models.Dto
{
    public class UserDto
    {
        public string? Username { get; set; }
        public string? PasswordHash { get; set; }
        public string? Token { get; set; }
        public string? Role { get; set; }
    }
}
