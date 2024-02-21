using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SastWiki.Core.Contracts.Backend;
using SastWiki.Core.Contracts.User;
using SastWiki.Core.Models.Dto;

namespace SastWiki.Core.Services.User
{
    public class UserLogin(IAuthenticationStorage _authentication, ISastWikiAPI _api) : IUserLogin
    {
        public async Task<UserDto> LoginAsync(string Email, string PasswordHash)
        {
            var loginResponse = await _api.Login(Email, PasswordHash);
            if (loginResponse.IsSuccessStatusCode && loginResponse.Content is not null)
            {
                UserDto loggedinUser = new UserDto
                {
                    Email = Email,
                    PasswordHash = PasswordHash,
                    Token = loginResponse.Content
                };
                _authentication.CurrentUser = loggedinUser;
                return loggedinUser;
            }
            else
            {
                throw loginResponse.Error ?? new Exception("Unknown Error when logging in");
            }
        }

        public async Task<UserDto> LogoutAsync()
        {
            var logoutResponse = await _api.Quit();
            if (logoutResponse.IsSuccessStatusCode)
            {
                var loggedoutUser = new UserDto();
                _authentication.CurrentUser = loggedoutUser;
                return loggedoutUser;
            }
            else
            {
                throw logoutResponse.Error ?? new Exception("Unknown Error when logging out");
            }
        }
    }
}
