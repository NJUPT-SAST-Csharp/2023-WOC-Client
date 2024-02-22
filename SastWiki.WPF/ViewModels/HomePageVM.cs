using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SastWiki.Core.Contracts.Backend.Entry;
using SastWiki.Core.Contracts.Infrastructure.CacheService;
using SastWiki.Core.Contracts.Infrastructure.SettingsService;
using SastWiki.WPF.Contracts;
using SastWiki.WPF.Views.Pages;

namespace SastWiki.WPF.ViewModels
{
    public partial class HomePageVM : ObservableObject, INavigationAware
    {
        private INavigationService _navigationService;

        public HomePageVM(INavigationService navigationSevice)
        {
            _navigationService = navigationSevice;
        }

        private async void TestWebview2()
        {
            await _navigationService.NavigateTo<int>(App.GetService<EntryViewPage>(), 1);
        }

        public ICommand TestWebView2_Click => new RelayCommand(TestWebview2);

        Task<bool> INavigationAware.OnNavigatedFrom()
        {
            return Task.FromResult(true);
        }

        Task<bool> INavigationAware.OnNavigatedTo<T>(T parameters)
        {
            return Task.FromResult(true);
        }
    }
}
