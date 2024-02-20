using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;
using SastWiki.WPF.Views.Pages;
using SastWiki.WPF.ViewModels;
using SastWiki.WPF.Contracts;
using SastWiki.WPF.Services;
using SastWiki.Core.Contracts.InternalLink;
using SastWiki.Core.Services.InternalLink;
using SastWiki.Core.Services.Backend;
using SastWiki.Core.Contracts.User;
using SastWiki.Core.Services.User;

namespace SastWiki.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IHost Host { get; }

        public static T GetService<T>()
            where T : class
        {
            if ((App.Current as App)!.Host.Services.GetService(typeof(T)) is not T service)
            {
                throw new ArgumentException(
                    $"{typeof(T)} needs to be registered in ConfigureServices within App.xaml.cs."
                );
            }

            return service;
        }

        public App()
        {
            InitializeComponent();

            Host = Microsoft.Extensions.Hosting.Host
                .CreateDefaultBuilder()
                .UseContentRoot(AppContext.BaseDirectory)
                .ConfigureServices(
                    (context, services) =>
                    {
                        // Core.Contracts.Backend
                        services.AddSingleton<
                            Core.Contracts.Backend.Entry.IEntryProvider,
                            用于测试的一些文档
                        >(); // 仅仅用于测试，实际应用中应该使用真实的数据源
                        services.AddSingleton<
                            Core.Contracts.Backend.Category.ICategoryProvider,
                            Core.Services.Backend.Category.CategoryProvider
                        >();
                        services.AddSingleton<
                            Core.Contracts.Backend.Tag.ITagProvider,
                            Core.Services.Backend.Tag.TagProvider
                        >();
                        services.AddSingleton<
                            Core.Contracts.Backend.Image.IImageProvider,
                            Core.Services.Backend.Image.ImageProvider
                        >();

                        services.AddSingleton<
                            Core.Contracts.Backend.Entry.IEntryCache,
                            Core.Services.Backend.Entry.EntryCache
                        >();
                        services.AddSingleton<
                            Core.Contracts.Backend.Category.ICategoryCache,
                            Core.Services.Backend.Category.CategoryCache
                        >();
                        services.AddSingleton<
                            Core.Contracts.Backend.Tag.ITagCache,
                            Core.Services.Backend.Tag.TagCache
                        >();
                        services.AddSingleton<
                            Core.Contracts.Backend.Image.IImageCache,
                            Core.Services.Backend.Image.ImageCache
                        >();

                        // Core.Contracts.Infrastructure
                        services.AddSingleton<
                            Core.Contracts.Infrastructure.CacheService.ICacheStorage,
                            Core.Services.Infrastructure.CacheService.CacheStorage
                        >();
                        services.AddSingleton<
                            Core.Contracts.Infrastructure.SettingsService.ISettingsProvider,
                            Core.Services.Infrastructure.SettingsService.SettingsProvider
                        >();
                        services.AddSingleton<
                            Core.Contracts.Infrastructure.SettingsService.ISettingsStorage,
                            Core.Services.Infrastructure.SettingsService.SettingsStorage
                        >();
                        services.AddSingleton<
                            Core.Contracts.Infrastructure.ILocalStorage,
                            Core.Services.Infrastructure.LocalStorage
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

                        // WPF.Contracts
                        services.AddSingleton<INavigationService, NavigationService>();
                        services.AddSingleton<IMarkdownProcessor, MarkdownProcessor>();

                        // Register ViewModels
                        services.AddSingleton<MainWindowVM>();
                        services.AddSingleton<HomePageVM>();
                        services.AddSingleton<BrowsePageVM>();
                        services.AddSingleton<EditPageVM>();
                        services.AddSingleton<SettingsVM>();
                        services.AddTransient<SearchResultVM>();
                        services.AddTransient<EntryViewVM>();

                        // Register Views
                        services.AddSingleton<MainWindow>();
                        services.AddSingleton<HomePage>();
                        services.AddSingleton<EditPage>();
                        services.AddSingleton<BrowsePage>();
                        services.AddSingleton<SettingsPage>();
                        services.AddSingleton<ThemeChangePage>();
                        services.AddSingleton<AboutMorePage>();
                        services.AddTransient<SearchResultPage>();
                        services.AddTransient<EntryViewPage>();
                    }
                )
                .Build();

            // Register Internal Links
            var internalLinkService = GetService<IInternalLinkService>();
            internalLinkService.Register(
                "/Home",
                (sender, e) =>
                {
                    var navigationService = GetService<INavigationService>();
                    navigationService.NavigateTo(GetService<HomePage>());
                }
            );

            internalLinkService.Register(
                "/Entry",
                (sender, e) =>
                {
                    if (int.TryParse(e["id"], out var id))
                    {
                        var navigationService = GetService<INavigationService>();
                        navigationService.NavigateTo(GetService<EntryViewPage>(), id);
                    }
                }
            );
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            this.MainWindow = GetService<MainWindow>();
            MainWindow.Show();
        }
    }
}
