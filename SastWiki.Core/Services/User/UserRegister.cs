using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SastWiki.Core.Contracts.Backend;
using SastWiki.Core.Contracts.User;
using SastWiki.Core.Models.Dto;

namespace SastWiki.Core.Services.User
{
    public class UserRegister(ISastWikiAPI api) : IUserRegister
    {
        public async Task RegisterAsync(string Username, string email, string PasswordHash)
        {
            var a = await api.Signup(Username, email, PasswordHash);
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
}
