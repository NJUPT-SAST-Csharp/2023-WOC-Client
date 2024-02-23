using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json.Linq;
using YamlDotNet.Core.Tokens;

namespace SastWiki.WPF.UserControl
{
    /// <summary>
    /// TagViewer.xaml 的交互逻辑
    /// </summary>
    [ObservableObject]
    public partial class TagViewer : System.Windows.Controls.UserControl
    {
        public TagViewer()
        {
            InitializeComponent();
        }

        [ObservableProperty]
        public List<TagViewerTagItem> tagViewerTags;

        public List<string> Tags
        {
            get { return (List<string>)GetValue(TagsProperty); }
            set
            {
                SetValue(TagsProperty, value);
                SetProperty(
                    ref tagViewerTags,
                    value
                        .Select(x => new TagViewerTagItem() { Name = x, ClickCommand = Click })
                        .ToList(),
                    "TagViewerTags"
                );
                OnPropertyChanged("TagViewerTags");
            }
        }

        public static readonly DependencyProperty TagsProperty = DependencyProperty.Register(
            "Tags",
            typeof(List<string>),
            typeof(TagViewer),
            new PropertyMetadata(new List<string>())
        );

        public ICommand Click
        {
            get { return (ICommand)GetValue(ClickProperty); }
            set { SetValue(ClickProperty, value); }
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
}
