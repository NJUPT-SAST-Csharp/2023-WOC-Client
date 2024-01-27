using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;
using SastWiki.WPF.Views.Pages;
using SastWiki.WPF.ViewModels;
using SastWiki.WPF.Contracts;
using SastWiki.WPF.Services;

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
                        services.AddSingleton<IPageService, PageService>();
                        services.AddSingleton<INavigationService, NavigationService>();

                        // Register ViewModels
                        services.AddSingleton<MainWindowVM>();
                        services.AddSingleton<HomePageVM>();
                        services.AddSingleton<BrowsePageVM>();
                        services.AddSingleton<SettingsVM>();

                        // Register Views
                        services.AddSingleton<MainWindow>();
                        services.AddSingleton<HomePage>();
                        services.AddSingleton<BrowsePage>();
                        services.AddSingleton<SettingsPage>();
                    }
                )
                .Build();

            // Register Pages to PageService
            var pageService = GetService<IPageService>();
            pageService.Configure<HomePageVM, HomePage>();
            pageService.Configure<BrowsePageVM, BrowsePage>();
            pageService.Configure<SettingsVM, SettingsPage>();
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            this.MainWindow = GetService<MainWindow>();
            MainWindow.Show();
        }
    }
}
