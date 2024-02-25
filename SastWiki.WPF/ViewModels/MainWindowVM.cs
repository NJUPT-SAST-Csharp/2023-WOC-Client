using System.Windows;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Refit;
using SastWiki.Core.Contracts.Backend.Category;
using SastWiki.Core.Contracts.Backend.Entry;
using SastWiki.Core.Models.Dto;
using SastWiki.Core.Models.Messages;
using SastWiki.WPF.Contracts;
using SastWiki.WPF.Models;
using SastWiki.WPF.Views.Pages;

namespace SastWiki.WPF.ViewModels;

public partial class MainWindowVM
    : ObservableObject,
        IRecipient<UserLoginStatusChangedMessage>,
        IRecipient<EntryMetadataChangedMessage>
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
        WeakReferenceMessenger.Default.RegisterAll(this);
    }

    [ObservableProperty]
    private bool isDarkMode = false;

    [ObservableProperty]
    private int selectedPageIndex = 0;

    [ObservableProperty]
    private string searchBoxText = "";

    private async void NavigateTo_HomePage() =>
        await _navigationService.NavigateTo(App.GetService<HomePage>());

    private async void NavigateTo_EditPage() =>
        await _navigationService.NavigateTo(App.GetService<EditPage>(), 0);

    private async void NavigateTo_SettingsPage() =>
        await _navigationService.NavigateTo(App.GetService<SettingsPage>());

    private async void NavigateTo_SearchResultPage() =>
        // MessageBox.Show(SearchBoxText);
        await _navigationService.NavigateTo(App.GetService<SearchResultPage>(), SearchBoxText);

    private void NavigateTo_UserPage()
    {
        UsernameWindow usernameWindow = new();
        _ = usernameWindow.ShowDialog();
    }

    public ICommand GoToHomePageCommand => new RelayCommand(NavigateTo_HomePage);
    public ICommand GoToSettingsPageCommand => new RelayCommand(NavigateTo_SettingsPage);
    public ICommand GoToSearchResultPageCommand => new RelayCommand(NavigateTo_SearchResultPage);
    public ICommand GoToUserPageCommand => new RelayCommand(NavigateTo_UserPage);
    public ICommand GoToEditPageCommand => new RelayCommand(NavigateTo_EditPage);

    // TreeView Section
    [ObservableProperty]
    private List<TreeNode> _treeViewNodes = [];

    private async Task LoadTreeViewContentAsync()
    {
        try
        {
            IEnumerable<TreeNode> mainNodes = (await _categoryProvider.GetAllCategoryList()).Select(
                x => new TreeNode()
                {
                    NodeID = x,
                    ParentID = "",
                    NodeName = x,
                    Type = NodeType.Category
                }
            );
            IEnumerable<TreeNode> EntryNodes = (await _entryProvider.GetEntryMetadataList()).Select(
                x => new TreeNode()
                {
                    NodeID = x.Id.ToString()!, // 由GetEntryMetadataList()获取的EntryID必不为null
                    ParentID = x.CategoryName ?? "",
                    NodeName = x.Title ?? "Untitled",
                    Type = NodeType.Entry
                }
            );
            TreeViewNodes = getNodes("", mainNodes.Concat(EntryNodes).ToList());
            OnPropertyChanged(nameof(TreeViewNodes));
        }
        catch (ApiException e)
        {
            _ = MessageBox.Show(e.Message, e.Content);
        }
        catch (Exception e)
        {
            _ = MessageBox.Show(e.Message);
        }
    }

    private List<TreeNode> getNodes(string parentID, List<TreeNode> nodes)
    {
        var mainNodes = nodes.Where(x => x.ParentID == parentID).ToList();
        var otherNodes = nodes.Where(x => x.ParentID != parentID).ToList();
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
                        _ = _navigationService.NavigateTo(
                            App.GetService<EntryViewPage>(),
                            int.Parse(node.NodeID)
                        );
                    }
                }
            }
        );

    public async void Receive(EntryMetadataChangedMessage message) =>
        // MessageBox.Show($"Received Message! EntryMetadataChanged!");
        await LoadTreeViewContentAsync();

    // User Status

    [ObservableProperty]
    private UserDto _currentUser = new();

    public void Receive(UserLoginStatusChangedMessage message) =>
        // MessageBox.Show($"Received Message! {message.Value}");
        CurrentUser = message.Value; // OnPropertyChanged(nameof(CurrentUser));
}
