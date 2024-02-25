using System.Windows.Controls;
using SastWiki.WPF.ViewModels;

namespace SastWiki.WPF.Views.Pages;

/// <summary>
/// EditPage.xaml 的交互逻辑
/// </summary>
public partial class EditPage : Page
{
    public EditPage(EditPageVM vm)
    {
        InitializeComponent();
        DataContext = vm;
    }
}
