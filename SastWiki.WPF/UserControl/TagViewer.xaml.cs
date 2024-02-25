using System.Windows;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;

namespace SastWiki.WPF.UserControl;

/// <summary>
/// TagViewer.xaml 的交互逻辑
/// </summary>
[ObservableObject]
public partial class TagViewer : System.Windows.Controls.UserControl
{
    public TagViewer() => InitializeComponent();

    public List<string> Tags
    {
        get => (List<string>)GetValue(TagsProperty);
        set => SetValue(TagsProperty, value);
    }

    public static readonly DependencyProperty TagsProperty = DependencyProperty.Register(
        "Tags",
        typeof(List<string>),
        typeof(TagViewer),
        new PropertyMetadata(new List<string>())
    );

    public ICommand Click
    {
        get => (ICommand)GetValue(ClickProperty);
        set => SetValue(ClickProperty, value);
    }

    // Using a DependencyProperty as the backing store for Click.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty ClickProperty = DependencyProperty.Register(
        "Click",
        typeof(ICommand),
        typeof(TagViewer)
    );
}

public record TagViewerTagItem()
{
    public required string Name { get; init; }
    public ICommand? ClickCommand { get; init; }
}
