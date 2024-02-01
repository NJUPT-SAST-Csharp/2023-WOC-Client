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
using System.Windows.Shapes;

namespace SastWiki.WPF
{
    /// <summary>
    /// RegisterWindow.xaml 的交互逻辑
    /// </summary>
    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
        {
            InitializeComponent();
        }

        private void Register(object sender, RoutedEventArgs e)
        {
            string username = NewUsernameTextBox.Text;
            string password = NewPasswordBox.Password;
            string ConfirmPassword = ConfirmPasswordBox.Password;
            if(password == ConfirmPassword)
            {
                //存储用户信息( )；
            }
            else
            {
                MessageBox.Show("Resgiter again");
            }
        }
    }
}
