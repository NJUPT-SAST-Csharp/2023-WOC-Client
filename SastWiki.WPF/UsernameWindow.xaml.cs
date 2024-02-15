using SastWiki.WPF.Contracts;
using SastWiki.WPF.Views.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SastWiki.WPF
{
    /// <summary>
    /// UsernameWIndow.xaml 的交互逻辑
    /// </summary>
    public partial class UsernameWindow : Window
    {
        public UsernameWindow()
        {
            InitializeComponent();
        }
        private void NavigateTo_LoginPage(object sender, RoutedEventArgs e)
        {
            LoginPage loginPage = new LoginPage();
            ContentFrame.Navigate(loginPage);
            Login.Visibility = Visibility.Collapsed;
        }
    }
}
