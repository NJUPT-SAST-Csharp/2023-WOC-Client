using SastWiki.Core.Contracts.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SastWiki.Core.Services.User
{
    public class UserStatus : IUserStatus
    {
        public UserStatus() { }

        public bool IsLoggedIn => throw new NotImplementedException();

        public string? Username => throw new NotImplementedException();

        public DateTime? LoginTime => throw new NotImplementedException();

        public DateTime? LoginExpirationTime => throw new NotImplementedException();

        public string? Role => throw new NotImplementedException();
    }
}
