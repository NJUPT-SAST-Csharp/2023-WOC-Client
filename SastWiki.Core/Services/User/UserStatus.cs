using SastWiki.Core.Contracts.Backend;
using SastWiki.Core.Contracts.User;
using SastWiki.Core.Models.Dto;

namespace SastWiki.Core.Services.User;

public class UserStatus(IAuthenticationStorage _authentication, ISastWikiAPI _api) : IUserStatus
{
    public UserDto GetUserStatus()
    {
        UserDto? currentUser = _authentication.CurrentUser;
        return currentUser is null
            ? new UserDto()
            : new UserDto()
            {
                Email = currentUser.Email,
                Role = currentUser.Role,
                Username = currentUser.Username
            };
    }

    public bool IsUserLoggedin() => _authentication.CurrentUser.Token != string.Empty;

    Task<UserDto> IUserStatus.GetUserStatus() => throw new NotImplementedException();

    Task<bool> IUserStatus.IsUserLoggedin() => throw new NotImplementedException();
}
