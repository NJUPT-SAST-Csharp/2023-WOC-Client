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
    /// LoginPage.xaml 的交互逻辑
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void NavigateTo_RegisterPage(object sender, RoutedEventArgs e)
        {
            RegisterPage registerPage = new RegisterPage();
            Frame.Navigate(registerPage);
            usernameTextBox.Visibility = Visibility.Collapsed;
            NewPasswordBox.Visibility = Visibility.Collapsed;
            username1.Visibility = Visibility.Collapsed;
            P1.Visibility = Visibility.Collapsed;
            enter.Visibility = Visibility.Collapsed;
            rememberPasswordCheckBox.Visibility = Visibility.Collapsed;
            register.Visibility = Visibility.Collapsed;
        }
    }
}
