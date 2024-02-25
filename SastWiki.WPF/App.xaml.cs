using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Refit;
using SastWiki.Core.Contracts.InternalLink;
using SastWiki.Core.Contracts.User;
using SastWiki.Core.Models;
using SastWiki.WPF.Contracts;
using SastWiki.WPF.Services;
using SastWiki.WPF.Utils;
using SastWiki.WPF.ViewModels;
using SastWiki.WPF.Views.Pages;

namespace SastWiki.WPF;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private IHost Host { get; }

    public static T GetService<T>()
        where T : class =>
        (App.Current as App)!.Host.Services.GetService(typeof(T)) is not T service
            ? throw new ArgumentException(
                $"{typeof(T)} needs to be registered in ConfigureServices within App.xaml.cs."
            )
            : service;

    public App()
    {
        InitializeComponent();

        Host = Microsoft
            .Extensions.Hosting.Host.CreateDefaultBuilder()
            .UseContentRoot(AppContext.BaseDirectory)
            .ConfigureServices(
                (context, services) =>
                {
                    _ = services.AddOptions();
                    _ = services.Configure<AppOptions>(
                        context.Configuration.GetSection("AppOptions")
                    );

                    // Core Services
                    Core.Helper.ServicesHelper.SetServices(services);

                    _ = services
                        .AddRefitClient<SastWiki.Core.Contracts.Backend.ISastWikiAPI>(
                            provider => new RefitSettings()
                            {
                                AuthorizationHeaderValueGetter = (_, ct) =>
                                    SastWiki.Core.Helper.RefitAuthBearerTokenFactory.GetBearerTokenAsync(
                                        ct
                                    )
                            }
                        )
                        .ConfigureHttpClient(c =>
                            c.BaseAddress = new Uri(
                                App.GetService<IOptions<AppOptions>>().Value.ServerURI
                            )
                        );

                    // WPF.Contracts
                    _ = services.AddSingleton<INavigationService, NavigationService>();
                    _ = services.AddSingleton<IMarkdownProcessor, MarkdownProcessor>();
                    _ = services.AddSingleton<MarkdownCSSProvider>();

                    // Register ViewModels
                    _ = services.AddSingleton<MainWindowVM>();
                    _ = services.AddSingleton<HomePageVM>();
                    _ = services.AddSingleton<EditPageVM>();
                    _ = services.AddSingleton<SettingsVM>();
                    _ = services.AddTransient<SearchResultVM>();
                    _ = services.AddTransient<EntryViewVM>();
                    _ = services.AddSingleton<LoginPageVM>();
                    _ = services.AddSingleton<RegisterPageVM>();
                    _ = services.AddSingleton<SystemSettingsVM>();

                    // Register Views
                    _ = services.AddSingleton<MainWindow>();
                    _ = services.AddSingleton<HomePage>();
                    _ = services.AddSingleton<EditPage>();
                    _ = services.AddSingleton<SettingsPage>();
                    _ = services.AddSingleton<ThemeChangePage>();
                    _ = services.AddSingleton<AboutMorePage>();
                    _ = services.AddTransient<SearchResultPage>();
                    _ = services.AddTransient<EntryViewPage>();
                    _ = services.AddSingleton<LoginPage>();
                    _ = services.AddSingleton<RegisterPage>();
                    _ = services.AddSingleton<SystemSettingsPage>();
                }
            )
            .Build();

        // Create Folders if not existed
        IOptions<AppOptions> options = GetService<IOptions<AppOptions>>();
        try
        {
            AppOptions appOptions = options.Value;

            // Check if SettingsFilePath exists, if not, create it
            if (!System.IO.Directory.Exists(appOptions.SettingsFilePath))
            {
                _ = System.IO.Directory.CreateDirectory(appOptions.SettingsFilePath);
            }

            // Check if CacheBasePath exists, if not, create it
            if (!System.IO.Directory.Exists(appOptions.CacheBasePath))
            {
                _ = System.IO.Directory.CreateDirectory(appOptions.CacheBasePath);
            }
        }
        catch (Exception)
        {
            _ = MessageBox.Show(
                "Error while creating folders. Please check if the application has the necessary permissions to create folders in the application directory.",
                "Error",
                MessageBoxButton.OK,
                MessageBoxImage.Error
            );
            throw;
        }

        // Register Refit Authentication Handler
        Core.Helper.ServicesHelper.SetRefitBearerTokenGetter(GetService<IAuthenticationStorage>());

        // Register Internal Links
        IInternalLinkService internalLinkService = GetService<IInternalLinkService>();
        _ = internalLinkService.Register(
            "/Home",
            (sender, e) =>
            {
                INavigationService navigationService = GetService<INavigationService>();
                _ = navigationService.NavigateTo(GetService<HomePage>());
            }
        );

        _ = internalLinkService.Register(
            "/Entry",
            (sender, e) =>
            {
                if (int.TryParse(e["id"], out int id))
                {
                    INavigationService navigationService = GetService<INavigationService>();
                    _ = navigationService.NavigateTo(GetService<EntryViewPage>(), id);
                }
            }
        );

        _ = internalLinkService.Register(
            "/Edit",
            (sender, e) =>
            {
                if (int.TryParse(e["id"], out int id))
                {
                    INavigationService navigationService = GetService<INavigationService>();
                    _ = navigationService.NavigateTo(GetService<EditPage>(), id);
                }
            }
        );
    }

    private void Application_Startup(object sender, StartupEventArgs e)
    {
        MainWindow = GetService<MainWindow>();
        MainWindow.Show();
    }
}
