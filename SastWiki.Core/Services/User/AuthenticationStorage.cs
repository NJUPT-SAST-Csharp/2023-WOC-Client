using SastWiki.Core.Contracts.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SastWiki.Core.Services.User
{
    public class AuthenticationStorage : IAuthenticationStorage
    {
        public AuthenticationStorage() { }

        string? IAuthenticationStorage.AuthenticationToken
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }
        string? IAuthenticationStorage.Username
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }
        string? IAuthenticationStorage.PasswordHash
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }
    }
}
