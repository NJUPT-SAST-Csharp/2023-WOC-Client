using SastWiki.Core.Contracts.Backend;
using SastWiki.Core.Contracts.User;

namespace SastWiki.Core.Services.User;

public class UserRegister(ISastWikiAPI api) : IUserRegister
{
    public async Task RegisterAsync(string Username, string email, string PasswordHash)
    {
        Refit.IApiResponse<string> a = await api.Signup(Username, email, PasswordHash);
        if (a.IsSuccessStatusCode)
        {
            //注册成功
        }
        else
        {
            //注册失败
            throw a.Error;
        }
    }
}
