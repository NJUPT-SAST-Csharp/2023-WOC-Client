using SastWiki.Core.Contracts.Backend.Tag;
using SastWiki.Core.Contracts.InternalLink;
using SastWiki.Core.Services.Backend.Tag;
using SastWiki.Core.Services.InternalLink;
using SastWiki.WPF.Contracts;
using SastWiki.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
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

namespace SastWiki.WPF.Views.Pages
{
    /// <summary>
    /// BrowsePage.xaml 的交互逻辑
    /// </summary>
    public partial class BrowsePage : Page
    {
        private readonly InternalLinkService _internalLinkService;
        private ITagProvider _tagProvider;
        private INavigationService _navigationService;

        public BrowsePage(ITagProvider tagProvider)
        {
            _tagProvider = tagProvider;
            InitializeComponent();
        }

        public async void LoadData()
        {
            var allTags = await _tagProvider.GetAllTagsList();

            var tagEntries = new List<TagEntry>();
            foreach (var tag in allTags)
            {
                var entryIds = await _tagProvider.GetEntryIdListByTag(tag);
                tagEntries.Add(new TagEntry { TagName = tag, Ids = entryIds });
            }

            lv.ItemsSource = tagEntries;
        }

        private void ListViewItem_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var item = sender as ListViewItem;
            if (item != null)
            {
                var tagEntry = item.Content as TagEntry;
                if (tagEntry != null)
                {
                    int Id = tagEntry.Ids.FirstOrDefault(); // 获取词条的 ID
                    _navigationService.NavigateTo<int>(App.GetService<EntryViewPage>(), Id);
                }
            }
        }
    }
}
