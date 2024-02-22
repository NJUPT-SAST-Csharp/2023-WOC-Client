using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SastWiki.Core.Contracts.Infrastructure.SettingsService;
using SastWiki.Core.Contracts.User;
using SastWiki.Core.Models.Dto;

namespace SastWiki.Core.Services.User
{
    public class AuthenticationStorage(ISettingsProvider _settings) : IAuthenticationStorage
    {
        object currentUserLock = new object();
        private UserDto _currentUser = new UserDto();

        public UserDto CurrentUser
        {
            get
            {
                _ = _settings
                    .GetItem<UserDto>("CurrentUser")
                    .ContinueWith(
                        (task) =>
                        {
                            if (task.Result is not null)
                                lock (currentUserLock)
                                {
                                    _currentUser = task.Result;
                                }
                        }
                    );
                lock (currentUserLock)
                {
                    return _currentUser;
                }
            }
            set
            {
                lock (currentUserLock)
                {
                    _currentUser = value ?? new UserDto();
                }
                _settings.SetItem("CurrentUser", _currentUser);
            }
        }
    }
}
