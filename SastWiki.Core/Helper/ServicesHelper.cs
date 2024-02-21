using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using SastWiki.Core.Contracts.InternalLink;
using SastWiki.Core.Contracts.User;
using SastWiki.Core.Services.InternalLink;
using SastWiki.Core.Services.User;

namespace SastWiki.Core.Helper
{
    public class ServicesHelper
    {
        public static void SetServices(IServiceCollection services)
        {
            services
                .AddRefitClient<SastWiki.Core.Contracts.Backend.ISastWikiAPI>(
                    provider => new RefitSettings()
                    {
                        AuthorizationHeaderValueGetter = (_, ct) =>
                            SastWiki.Core.Helper.RefitAuthBearerTokenFactory.GetBearerTokenAsync(ct)
                    }
                )
                .ConfigureHttpClient(c => c.BaseAddress = new Uri("http://localhost:5281/"));

            // Core.Contracts.Backend
            services.AddSingleton<
                SastWiki.Core.Contracts.Backend.Entry.IEntryProvider,
                SastWiki.Core.Services.Backend.Entry.EntryProvider
            >(); // 仅仅用于测试，实际应用中应该使用真实的数据源
            services.AddSingleton<
                SastWiki.Core.Contracts.Backend.Category.ICategoryProvider,
                SastWiki.Core.Services.Backend.Category.CategoryProvider
            >();
            services.AddSingleton<
                SastWiki.Core.Contracts.Backend.Tag.ITagProvider,
                SastWiki.Core.Services.Backend.Tag.TagProvider
            >();
            services.AddSingleton<
                SastWiki.Core.Contracts.Backend.Image.IImageProvider,
                SastWiki.Core.Services.Backend.Image.ImageProvider
            >();

            services.AddSingleton<
                SastWiki.Core.Contracts.Backend.Entry.IEntryCache,
                SastWiki.Core.Services.Backend.Entry.EntryCache
            >();
            services.AddSingleton<
                SastWiki.Core.Contracts.Backend.Category.ICategoryCache,
                SastWiki.Core.Services.Backend.Category.CategoryCache
            >();
            services.AddSingleton<
                SastWiki.Core.Contracts.Backend.Tag.ITagCache,
                SastWiki.Core.Services.Backend.Tag.TagCache
            >();
            services.AddSingleton<
                SastWiki.Core.Contracts.Backend.Image.IImageCache,
                SastWiki.Core.Services.Backend.Image.ImageCache
            >();

            // Core.Contracts.Infrastructure
            services.AddSingleton<
                SastWiki.Core.Contracts.Infrastructure.CacheService.ICacheStorage,
                SastWiki.Core.Services.Infrastructure.CacheService.CacheStorage
            >();
            services.AddSingleton<
                SastWiki.Core.Contracts.Infrastructure.SettingsService.ISettingsProvider,
                SastWiki.Core.Services.Infrastructure.SettingsService.SettingsProvider
            >();
            services.AddSingleton<
                SastWiki.Core.Contracts.Infrastructure.SettingsService.ISettingsStorage,
                SastWiki.Core.Services.Infrastructure.SettingsService.SettingsStorage
            >();
            services.AddSingleton<
                SastWiki.Core.Contracts.Infrastructure.ILocalStorage,
                SastWiki.Core.Services.Infrastructure.LocalStorage
            >();

            // Core.Contracts.InternalLink
            services.AddSingleton<IInternalLinkService, InternalLinkService>();
            services.AddSingleton<IInternalLinkHandler, InternalLinkHandler>();
            services.AddSingleton<IInternalLinkValidator, InternalLinkValidator>();
            services.AddSingleton<IInternalLinkCreator, InternalLinkCreator>();

            // Core.Contracts.User
            services.AddSingleton<IAuthenticationStorage, AuthenticationStorage>();
            services.AddSingleton<IUserLogin, UserLogin>();
            services.AddSingleton<IUserRegister, UserRegister>();
            services.AddSingleton<IUserStatus, UserStatus>();
        }

        public static void SetRefitBearerTokenGetter(IAuthenticationStorage authenticationStorage)
        {
            SastWiki.Core.Helper.RefitAuthBearerTokenFactory.SetBearerTokenGetterFunc(async _ =>
                authenticationStorage.CurrentUser.Token ?? ""
            );
        }
    }
}
