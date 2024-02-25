using System.Windows.Controls;
using SastWiki.WPF.ViewModels;

namespace SastWiki.WPF.Views.Pages;

/// <summary>
/// HomePage.xaml 的交互逻辑
/// </summary>
public partial class HomePage : Page
{
    public HomePage(HomePageVM ViewModel)
    {
        DataContext = ViewModel;
        InitializeComponent();
    }
}
