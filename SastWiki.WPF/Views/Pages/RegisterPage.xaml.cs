using System.Windows.Controls;
using SastWiki.WPF.ViewModels;

namespace SastWiki.WPF.Views.Pages;

/// <summary>
/// RegisterPage.xaml 的交互逻辑
/// </summary>
public partial class RegisterPage : Page
{
    public RegisterPage(RegisterPageVM vm)
    {
        InitializeComponent();
        DataContext = vm;
    }
}
