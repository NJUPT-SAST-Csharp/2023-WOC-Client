using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SastWiki.WPF.Contracts;
using SastWiki.WPF.Views.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SastWiki.WPF.ViewModels
{
    public partial class MainWindowVM : ObservableObject
    {
        private INavigationService _navigationService;

        public MainWindowVM(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        [ObservableProperty]
        private bool isDarkMode = false;

        [ObservableProperty]
        private int selectedPageIndex = 0;

        [ObservableProperty]
        private string searchBoxText = String.Empty;

        private async void NavigateTo_HomePage() =>
            await _navigationService.NavigateTo(App.GetService<HomePage>());

        private async void NavigateTo_BrowsePage() =>
            await _navigationService.NavigateTo(App.GetService<BrowsePage>());

        private async void NavigateTo_SettingsPage() =>
            await _navigationService.NavigateTo(App.GetService<SettingsPage>());

        private async void NavigateTo_SearchResultPage() =>
            await _navigationService.NavigateTo(App.GetService<SearchResultPage>(), SearchBoxText);

        private async void NavigateTo_EditPage() =>
            await _navigationService.NavigateTo(App.GetService<EditPage>(), SearchBoxText);

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
        public ICommand GotoEditPageCommand => new RelayCommand(NavigateTo_EditPage);
    }
}
