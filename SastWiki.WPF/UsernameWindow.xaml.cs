using System.Windows;
using SastWiki.WPF.ViewModels;

namespace SastWiki.WPF;

/// <summary>
/// UsernameWIndow.xaml 的交互逻辑
/// </summary>
public partial class UsernameWindow : Window
{
    public UsernameWindow()
    {
        InitializeComponent();
        DataContext = new UsernameWindowVM();
    }
}
