using SastWiki.Core.Contracts.User;
using SastWiki.Core.Models.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SastWiki.Core.Services.User
{
    public class UserLogin : IUserLogin
    {
        public UserLogin() { }

        public Task<UserLoginResult> LoginAsync(string Username, string PasswordHash)
        {
            throw new NotImplementedException();
        }

        public Task<UserLoginResult> LogoutAsync()
        {
            throw new NotImplementedException();
        }
    }
}
