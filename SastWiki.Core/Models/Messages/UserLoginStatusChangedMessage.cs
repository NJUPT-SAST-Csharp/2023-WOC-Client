using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Messaging.Messages;
using SastWiki.Core.Models.Dto;

namespace SastWiki.Core.Models.Messages
{
    public class UserLoginStatusChangedMessage(UserDto value)
        : ValueChangedMessage<UserDto>(value) { }
}
