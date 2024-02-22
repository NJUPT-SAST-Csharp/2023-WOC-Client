using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Xml.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SastWiki.Core.Contracts.Backend.Category;
using SastWiki.Core.Contracts.Backend.Entry;
using SastWiki.Core.Services.Backend.Category;
using SastWiki.WPF.Contracts;
using SastWiki.WPF.Models;
using SastWiki.WPF.Views.Pages;

namespace SastWiki.WPF.ViewModels
{
    public partial class MainWindowVM : ObservableObject
    {
        private readonly INavigationService _navigationService;
        private readonly IEntryProvider _entryProvider;
        private readonly ICategoryProvider _categoryProvider;

        public MainWindowVM(
            INavigationService navigationService,
            IEntryProvider entryProvider,
            ICategoryProvider categoryProvider
        )
        {
            _navigationService = navigationService;
            _entryProvider = entryProvider;
            _categoryProvider = categoryProvider;
            _ = LoadTreeViewContentAsync();
        }

        [ObservableProperty]
        private bool isDarkMode = false;

        [ObservableProperty]
        private int selectedPageIndex = 0;

        [ObservableProperty]
        private string searchBoxText = "";

        private async void NavigateTo_HomePage() =>
            await _navigationService.NavigateTo(App.GetService<HomePage>());

        private async void NavigateTo_BrowsePage() =>
            await _navigationService.NavigateTo(App.GetService<BrowsePage>());

        private async void NavigateTo_SettingsPage() =>
            await _navigationService.NavigateTo(App.GetService<SettingsPage>());

        private async void NavigateTo_SearchResultPage()
        {
            MessageBox.Show(SearchBoxText);
            await _navigationService.NavigateTo(App.GetService<SearchResultPage>(), SearchBoxText);
        }

        private async void NavigateTo_UserPage()
        {
            UsernameWindow usernameWindow = new UsernameWindow();
            usernameWindow.ShowDialog();
        }

        public ICommand GoToHomePageCommand => new RelayCommand(NavigateTo_HomePage);
        public ICommand GoToBrowsePageCommand => new RelayCommand(NavigateTo_BrowsePage);
        public ICommand GoToSettingsPageCommand => new RelayCommand(NavigateTo_SettingsPage);
        public ICommand GoToSearchResultPageCommand =>
            new RelayCommand(NavigateTo_SearchResultPage);
        public ICommand GoToUserPageCommand => new RelayCommand(NavigateTo_UserPage);

        // TreeView Section
        [ObservableProperty]
        private List<TreeNode> _treeViewNodes = [];

        private async Task LoadTreeViewContentAsync()
        {
            var mainNodes = (await _categoryProvider.GetAllCategoryList()).Select(
                x => new TreeNode()
                {
                    NodeID = x,
                    ParentID = "",
                    NodeName = x,
                    Type = NodeType.Category
                }
            );
            var EntryNodes = (await _entryProvider.GetEntryMetadataList()).Select(
                x => new TreeNode()
                {
                    NodeID = x.Id.ToString()!, // 由GetEntryMetadataList()获取的EntryID必不为null
                    ParentID = x.CategoryName ?? "",
                    NodeName = x.Title ?? "Untitled",
                    Type = NodeType.Entry
                }
            );
            TreeViewNodes = getNodes("", mainNodes.Concat(EntryNodes).ToList());
        }

        private List<TreeNode> getNodes(string parentID, List<TreeNode> nodes)
        {
            List<TreeNode> mainNodes = nodes.Where(x => x.ParentID == parentID).ToList();
            List<TreeNode> otherNodes = nodes.Where(x => x.ParentID != parentID).ToList();
            foreach (TreeNode node in mainNodes)
                node.ChildNodes = getNodes(node.NodeID, otherNodes);
            return mainNodes;
        }

        public ICommand SelectItemChangeCommand =>
            new RelayCommand<TreeNode>(
                (node) =>
                {
                    if (node is not null)
                    {
                        if (node.Type == NodeType.Category)
                        {
                            //    _navigationService.NavigateTo(App.GetService<BrowsePage>(), node.NodeName);
                        }
                        else
                        {
                            _navigationService.NavigateTo(
                                App.GetService<EntryViewPage>(),
                                int.Parse(node.NodeID)
                            );
                        }
                    }
                }
            );
    }
}
