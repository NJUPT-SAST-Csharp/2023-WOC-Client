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
    public class UserStatus(IAuthenticationStorage _authentication, ISastWikiAPI _api) : IUserStatus
    {
        public async Task<UserDto> GetUserStatus()
        {
            var currentUser = _authentication.CurrentUser;
            if (currentUser is null)
            {
                return new UserDto();
            }
            else
            {
                return new UserDto()
                {
                    Email = currentUser.Email,
                    Role = currentUser.Role,
                    Username = currentUser.Username
                };
            }
        }

        public async Task<bool> IsUserLoggedin()
        {
            return _authentication.CurrentUser.Token != String.Empty;
        }
    }
}
