using CommunityToolkit.Mvvm.Messaging.Messages;
using SastWiki.Core.Models.Dto;

namespace SastWiki.Core.Models.Messages;

public class UserLoginStatusChangedMessage(UserDto value) : ValueChangedMessage<UserDto>(value) { }
