using System.Windows;
using System.Windows.Controls;
using SastWiki.WPF.Contracts;

namespace SastWiki.WPF.Views.Pages;

/// <summary>
/// SettingsPage.xaml 的交互逻辑
/// </summary>
public partial class SettingsPage : Page
{
    private readonly INavigationService _navigationService;

    public SettingsPage(INavigationService navigationService)
    {
        _navigationService = navigationService;
        InitializeComponent();
    }

    private void NavigateTo_ThemeChangePage(object sender, RoutedEventArgs e) =>
        SettingsFrame.Navigate(App.GetService<ThemeChangePage>());

    private void NavigateTo_AboutMorePage(object sender, RoutedEventArgs e) =>
        SettingsFrame.Navigate(App.GetService<AboutMorePage>());

    private void NavigateTo_SystemSettingsPage(object sender, RoutedEventArgs e) =>
        SettingsFrame.Navigate(App.GetService<SystemSettingsPage>());
}
