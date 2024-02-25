using System.Windows.Controls;
using SastWiki.WPF.ViewModels;

namespace SastWiki.WPF.Views.Pages;

/// <summary>
/// SystemSettingsPage.xaml 的交互逻辑
/// </summary>
public partial class SystemSettingsPage : Page
{
    public SystemSettingsPage(SystemSettingsVM vm)
    {
        InitializeComponent();
        DataContext = vm;
    }
}
