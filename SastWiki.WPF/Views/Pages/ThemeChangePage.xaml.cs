using SastWiki.WPF.Contracts;
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
    /// ThemeChangePage.xaml 的交互逻辑
    /// </summary>
    public partial class ThemeChangePage : Page
    {
        private INavigationService _navigationService;

        public ThemeChangePage(INavigationService navigationService)
        {
            _navigationService = navigationService;
            InitializeComponent();
        }

        private void ThemeSwitch_Checked(object sender, RoutedEventArgs e)
        {
            ResourceDictionary resource = new ResourceDictionary();
            if (
                Application.Current.Resources.MergedDictionaries[0].Source.ToString()
                == ThemeDefault
            )
            {
                resource.Source = new Uri(ThemeDark);
            }
            else
            {
                resource.Source = new Uri(ThemeDefault);
            }
            Application.Current.Resources.MergedDictionaries[0] = resource;
        }

        private void NavigateTo_ThemeChangePage(object sender, RoutedEventArgs e) =>
            _navigationService.NavigateTo(App.GetService<ThemeChangePage>());

        private void NavigateTo_AboutMorePage(object sender, RoutedEventArgs e) =>
            _navigationService.NavigateTo(App.GetService<AboutMorePage>());

        private const string ThemeDark = "pack://application:,,,/Resource/Theme/GrayColor.xaml";
        private const string ThemeDefault = "pack://application:,,,/Resource/Theme/WhiteColor.xaml";
    }
}
