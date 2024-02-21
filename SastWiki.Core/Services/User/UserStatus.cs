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
            throw new NotImplementedException();
        }

        public async Task<bool> IsUserLoggedin()
        {
            throw new NotImplementedException();
        }
    }
}
