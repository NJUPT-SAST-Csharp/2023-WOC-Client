using SastWiki.Core.Contracts.Backend;
using SastWiki.Core.Contracts.User;
using SastWiki.Core.Models.Dto;

namespace SastWiki.Core.Services.User;

public class UserLogin(IAuthenticationStorage _authentication, ISastWikiAPI _api) : IUserLogin
{
    public async Task<UserDto> LoginAsync(string Email, string PasswordHash)
    {
        Refit.IApiResponse<string> loginResponse = await _api.Login(Email, PasswordHash);
        if (loginResponse.IsSuccessStatusCode && loginResponse.Content is not null)
        {
            var loggedinUser = new UserDto
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
        Refit.IApiResponse<string> logoutResponse = await _api.Quit();
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
