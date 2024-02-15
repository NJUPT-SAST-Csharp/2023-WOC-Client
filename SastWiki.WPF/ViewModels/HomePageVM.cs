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
    public partial class HomePageVM : ObservableObject, INavigationAware
    {
        private INavigationService _navigationService;

        public HomePageVM(INavigationService navigationSevice)
        {
            _navigationService = navigationSevice;
        }

        private void TestWebview2()
        {
            _navigationService.NavigateTo(App.GetService<EntryViewPage>(), 114514);
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
