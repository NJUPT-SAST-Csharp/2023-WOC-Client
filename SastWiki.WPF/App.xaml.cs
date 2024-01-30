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

        /*        public static object GetService(Type type)
                {
                    if ((App.Current as App)!.Host.Services.GetService(type) is not object service)
                    {
                        throw new ArgumentException(
                            $"{type} needs to be registered in ConfigureServices within App.xaml.cs."
                        );
                    }
        
                    return service;
                }*/

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
                        services.AddTransient<SearchResultPage>();
                        services.AddTransient<EntryViewPage>();
                    }
                )
                .Build();
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            this.MainWindow = GetService<MainWindow>();
            MainWindow.Show();
        }
    }
}
