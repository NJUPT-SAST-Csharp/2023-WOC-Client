using Microsoft.Extensions.DependencyInjection;
using SastWiki.Core.Contracts.InternalLink;
using SastWiki.Core.Contracts.User;
using SastWiki.Core.Services.InternalLink;
using SastWiki.Core.Services.User;

namespace SastWiki.Core.Helper;

public class ServicesHelper
{
    public static void SetServices(IServiceCollection services)
    {
        // Core.Contracts.Backend
        _ = services.AddSingleton<
            SastWiki.Core.Contracts.Backend.Entry.IEntryProvider,
            SastWiki.Core.Services.Backend.Entry.EntryProvider
        >(); // 仅仅用于测试，实际应用中应该使用真实的数据源
        _ = services.AddSingleton<
            SastWiki.Core.Contracts.Backend.Category.ICategoryProvider,
            SastWiki.Core.Services.Backend.Category.CategoryProvider
        >();
        _ = services.AddSingleton<
            SastWiki.Core.Contracts.Backend.Tag.ITagProvider,
            SastWiki.Core.Services.Backend.Tag.TagProvider
        >();
        _ = services.AddSingleton<
            SastWiki.Core.Contracts.Backend.Image.IImageProvider,
            SastWiki.Core.Services.Backend.Image.ImageProvider
        >();
        _ = services.AddSingleton<
            SastWiki.Core.Contracts.Backend.Entry.IEntryCache,
            SastWiki.Core.Services.Backend.Entry.EntryCache
        >();

        // Core.Contracts.Infrastructure
        _ = services.AddSingleton<
            SastWiki.Core.Contracts.Infrastructure.CacheService.ICacheStorage,
            SastWiki.Core.Services.Infrastructure.CacheService.CacheStorage
        >();
        _ = services.AddSingleton<
            SastWiki.Core.Contracts.Infrastructure.SettingsService.ISettingsProvider,
            SastWiki.Core.Services.Infrastructure.SettingsService.SettingsProvider
        >();
        _ = services.AddSingleton<
            SastWiki.Core.Contracts.Infrastructure.SettingsService.ISettingsStorage,
            SastWiki.Core.Services.Infrastructure.SettingsService.SettingsStorage
        >();
        _ = services.AddSingleton<
            SastWiki.Core.Contracts.Infrastructure.ILocalStorage,
            SastWiki.Core.Services.Infrastructure.LocalStorage
        >();

        // Core.Contracts.InternalLink
        _ = services.AddSingleton<IInternalLinkService, InternalLinkService>();
        _ = services.AddSingleton<IInternalLinkHandler, InternalLinkHandler>();
        _ = services.AddSingleton<IInternalLinkValidator, InternalLinkValidator>();
        _ = services.AddSingleton<IInternalLinkCreator, InternalLinkCreator>();

        // Core.Contracts.User
        _ = services.AddSingleton<IAuthenticationStorage, AuthenticationStorage>();
        _ = services.AddSingleton<IUserLogin, UserLogin>();
        _ = services.AddSingleton<IUserRegister, UserRegister>();
        _ = services.AddSingleton<IUserStatus, UserStatus>();
    }

    public static void SetRefitBearerTokenGetter(IAuthenticationStorage authenticationStorage) =>
        SastWiki.Core.Helper.RefitAuthBearerTokenFactory.SetBearerTokenGetterFunc(async _ =>
            authenticationStorage.CurrentUser.Token ?? ""
        );
}
