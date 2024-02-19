using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SastWiki.Core.Contracts.User;
using SastWiki.Core.Models.Dto;

namespace SastWiki.Core.Services.User
{
    public class UserLogin : IUserLogin
    {
        public UserLogin() { }

        public Task<UserDto> LoginAsync(string Username, string PasswordHash)
        {
            throw new NotImplementedException();
        }

        public Task<UserDto> LogoutAsync()
        {
            throw new NotImplementedException();
        }
    }
}
