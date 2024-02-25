using System.Windows.Controls;
using SastWiki.WPF.ViewModels;

namespace SastWiki.WPF.Views.Pages;

/// <summary>
/// LoginPage.xaml 的交互逻辑
/// </summary>
public partial class LoginPage : Page
{
    public LoginPage(LoginPageVM vm)
    {
        InitializeComponent();
        DataContext = vm;
    }
}
