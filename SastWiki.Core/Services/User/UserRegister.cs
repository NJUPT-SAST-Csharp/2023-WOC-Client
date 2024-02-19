using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SastWiki.Core.Contracts.User;
using SastWiki.Core.Models.Dto;

namespace SastWiki.Core.Services.User
{
    public class UserRegister : IUserRegister
    {
        public UserRegister() { }

        public Task<UserDto> Register(string Username, string PasswordHash)
        {
            throw new NotImplementedException();
        }
    }
}
