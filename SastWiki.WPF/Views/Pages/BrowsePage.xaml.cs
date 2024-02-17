using SastWiki.Core.Contracts.Backend.Tag;
using SastWiki.Core.Services.Backend.Tag;
using SastWiki.WPF.ViewModels;
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

namespace SastWiki.WPF.Views.Pages
{
    /// <summary>
    /// BrowsePage.xaml 的交互逻辑
    /// </summary>
    public partial class BrowsePage : Page
    {
        private ITagProvider _tagProvider;

        public BrowsePage(ITagProvider tagProvider)
        {
            _tagProvider = tagProvider;
            InitializeComponent();
        }

        public async void LoadData()
        {
            // 获取所有标签
            var allTags = await _tagProvider.GetAllTagsList();

            // 创建数据模型列表
            var tagEntries = new List<TagEntry>();
            foreach (var tag in allTags)
            {
                var entryIds = await _tagProvider.GetEntryIdListByTag(tag);
                tagEntries.Add(new TagEntry { TagName = tag, Ids = entryIds });
            }

            // 绑定到 ListView
            lv.ItemsSource = tagEntries;
        }

        private void ListViewItem_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var item = sender as ListViewItem;
            if (item != null)
            {
                // 处理点击事件
            }
        }
    }
}
