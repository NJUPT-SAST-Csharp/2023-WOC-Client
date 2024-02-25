using System.Windows.Controls;
using SastWiki.WPF.ViewModels;

namespace SastWiki.WPF.Views.Pages;

/// <summary>
/// EntryViewPage.xaml 的交互逻辑
/// </summary>
public partial class EntryViewPage : Page
{
    public EntryViewPage(EntryViewVM ViewModel)
    {
        DataContext = ViewModel;
        InitializeComponent();
        ViewModel.WebView = WebView;
    }
}
