using CommunityToolkit.Mvvm.Messaging;
using SastWiki.Core.Contracts.Infrastructure.SettingsService;
using SastWiki.Core.Contracts.User;
using SastWiki.Core.Models.Dto;
using SastWiki.Core.Models.Messages;

namespace SastWiki.Core.Services.User;

public class AuthenticationStorage(ISettingsProvider _settings) : IAuthenticationStorage
{
    private readonly object currentUserLock = new();
    private UserDto _currentUser = new();

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
                        {
                            lock (currentUserLock)
                            {
                                _currentUser = task.Result;
                            }
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

            _ = _settings.SetItem("CurrentUser", _currentUser);
            _ = WeakReferenceMessenger.Default.Send(
                new UserLoginStatusChangedMessage(_currentUser)
            );
        }
    }
}
