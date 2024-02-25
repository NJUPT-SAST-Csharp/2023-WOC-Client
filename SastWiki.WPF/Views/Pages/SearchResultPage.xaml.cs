using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using SastWiki.WPF.ViewModels;

namespace SastWiki.WPF.Views.Pages;

/// <summary>
/// SearchResultPage.xaml 的交互逻辑
/// </summary>
public partial class SearchResultPage : Page
{
    public SearchResultPage(SearchResultVM searchResultVM)
    {
        DataContext = searchResultVM;
        InitializeComponent();
    }

    private void ListBox_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
    {
        if (!e.Handled)
        {
            // ListView拦截鼠标滚轮事件
            e.Handled = true;

            // 激发一个鼠标滚轮事件，冒泡给外层ListView接收到
            var eventArg = new MouseWheelEventArgs(e.MouseDevice, e.Timestamp, e.Delta)
            {
                RoutedEvent = UIElement.MouseWheelEvent,
                Source = sender
            };
            var parent = ((Control)sender).Parent as UIElement;
            parent!.RaiseEvent(eventArg);
        }
    }
}
