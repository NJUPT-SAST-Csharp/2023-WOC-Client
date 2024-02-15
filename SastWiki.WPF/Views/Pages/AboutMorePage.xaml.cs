﻿using SastWiki.WPF.Contracts;
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
    /// AboutMorePage.xaml 的交互逻辑
    /// </summary>
    public partial class AboutMorePage : Page
    {
        private INavigationService _navigationService;

        public AboutMorePage(INavigationService navigationService)
        {
            _navigationService = navigationService;
            InitializeComponent();
        }

        private void NavigateTo_ThemeChangePage(object sender, RoutedEventArgs e) =>
            _navigationService.NavigateTo(App.GetService<ThemeChangePage>());

        private void NavigateTo_AboutMorePage(object sender, RoutedEventArgs e) =>
            _navigationService.NavigateTo(App.GetService<AboutMorePage>());
    }
}