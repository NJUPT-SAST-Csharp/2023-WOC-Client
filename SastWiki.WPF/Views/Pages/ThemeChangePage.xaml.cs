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
        public ThemeChangePage()
        {
            InitializeComponent();
        }

        private void ThemeSwitch_Checked(object sender, RoutedEventArgs e)
        {
            ResourceDictionary resource = new ResourceDictionary();
            var mw = App.GetService<MainWindow>();
            if (
                Application.Current.Resources.MergedDictionaries[0].Source.ToString()
                == ThemeDefault
            )
            {
                resource.Source = new Uri(ThemeDark);
                mw.IsDark = false;
                mw.RefreshDarkMode();
            }
            else
            {
                resource.Source = new Uri(ThemeDefault);
                mw.IsDark = true;
                mw.RefreshDarkMode();
            }
            Application.Current.Resources.MergedDictionaries[0] = resource;
        }

        private const string ThemeDark = "pack://application:,,,/Resource/Theme/GrayColor.xaml";
        private const string ThemeDefault = "pack://application:,,,/Resource/Theme/WhiteColor.xaml";
    }
}
