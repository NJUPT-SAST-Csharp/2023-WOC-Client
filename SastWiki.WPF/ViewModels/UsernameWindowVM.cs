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
using System.Windows.Controls;
using System.Windows.Input;

namespace SastWiki.WPF.ViewModels
{
    internal class UsernameWindowVM : ObservableObject
    {
        private RelayCommand navigateTo_LoginPage;
        public ICommand NavigateTo_LoginPage => navigateTo_LoginPage ??= new RelayCommand(PerformNavigateTo_LoginPage);
        private void PerformNavigateTo_LoginPage()
        {
            LoginPage loginPage = new LoginPage();
            CurrentPage = loginPage;
            LoginVisibility = Visibility.Collapsed;
        }
        private Visibility loginVisibility = Visibility.Visible;
        public Visibility LoginVisibility
        {
            get => loginVisibility;
            set => SetProperty(ref loginVisibility, value);
        }

        private object currentPage;
        public object CurrentPage { get => currentPage; set => SetProperty(ref currentPage, value); }
    }
}
