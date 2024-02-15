using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;
using SastWiki.WPF.Views.Pages;
using SastWiki.WPF.ViewModels;
using SastWiki.WPF.Contracts;
using SastWiki.WPF.Services;
using SastWiki.Core.Contracts;
using SastWiki.Core.Services;
using SastWiki.Core.Contracts.InternalLink;
using SastWiki.Core.Services.InternalLink;
using SastWiki.Core.Contracts.Backend;
using SastWiki.Core.Services.Backend;

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
                        // Register Services
                        services.AddSingleton<INavigationService, NavigationService>();
                        services.AddSingleton<IMarkdownProcessor, MarkdownProcessor>();
                        services.AddSingleton<IInternalLinkService, InternalLinkService>();
                        services.AddSingleton<IInternalLinkHandler, InternalLinkHandler>();
                        services.AddSingleton<IInternalLinkValidator, InternalLinkValidator>();
                        services.AddSingleton<IInternalLinkCreator, InternalLinkCreator>();

                        // Register ViewModels
                        services.AddSingleton<MainWindowVM>();
                        services.AddSingleton<HomePageVM>();
                        services.AddSingleton<BrowsePageVM>();
                        services.AddSingleton<SettingsVM>();
                        services.AddTransient<SearchResultVM>();
                        services.AddTransient<EntryViewVM>();
                        

                        // Register Views
                        services.AddSingleton<MainWindow>();
                        services.AddSingleton<HomePage>();
                        services.AddSingleton<BrowsePage>();
                        services.AddSingleton<SettingsPage>();
                        services.AddSingleton<ThemeChangePage>();
                        services.AddSingleton<AboutMorePage>();
                        services.AddTransient<SearchResultPage>();
                        services.AddTransient<EntryViewPage>();

                        // 仅仅用于测试，实际应用中应该使用真实的数据源
                        services.AddSingleton<IEntryProvider, 用于测试的一些文档>();
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
