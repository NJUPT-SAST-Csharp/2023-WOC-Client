using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;

namespace SastWiki.WPF.Views.Pages;

/// <summary>
/// InputTagDialog.xaml 的交互逻辑
/// </summary>
[ObservableObject]
public partial class InputTagDialog : Window
{
    public InputTagDialog() => InitializeComponent();

    private void AddTagButton_Click(object sender, RoutedEventArgs e) => DialogResult = true;

    public string GetText() => TagTextBox.Text;
}
