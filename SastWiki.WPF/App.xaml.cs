using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Refit;
using SastWiki.Core.Contracts.InternalLink;
using SastWiki.Core.Contracts.User;
using SastWiki.Core.Services.Backend;
using SastWiki.Core.Services.InternalLink;
using SastWiki.Core.Services.User;
using SastWiki.WPF.Contracts;
using SastWiki.WPF.Services;
using SastWiki.WPF.Utils;
using SastWiki.WPF.ViewModels;
using SastWiki.WPF.Views.Pages;

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

            Host = Microsoft
                .Extensions.Hosting.Host.CreateDefaultBuilder()
                .UseContentRoot(AppContext.BaseDirectory)
                .ConfigureServices(
                    (context, services) =>
                    {
                        // Core Services
                        Core.Helper.ServicesHelper.SetServices(services);

                        // WPF.Contracts
                        services.AddSingleton<INavigationService, NavigationService>();
                        services.AddSingleton<IMarkdownProcessor, MarkdownProcessor>();
                        services.AddSingleton<MarkdownCSSProvider>();

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

            // Register Refit Authentication Handler
            Core.Helper.ServicesHelper.SetRefitBearerTokenGetter(
                GetService<IAuthenticationStorage>()
            );

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

            internalLinkService.Register(
                "/Edit",
                (sender, e) =>
                {
                    if (int.TryParse(e["id"], out var id))
                    {
                        var navigationService = GetService<INavigationService>();
                        navigationService.NavigateTo(GetService<EditPage>(), id);
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
