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
    /// UsernameWindow.xaml 的交互逻辑
    /// </summary>
    public partial class UsernameWindow : Window
    {
        public UsernameWindow()
        {
            InitializeComponent();
        }

        private void username(object sender, RoutedEventArgs e)//登录
        {
            /*if(loginSucess)
            {
                //关闭登陆页面，进入HomePage;
            else
            {
                MessageBox.Show("登陆失败");
            }
            }*/
        }

        private void register(object sender, RoutedEventArgs e)//注册
        {
            RegisterWindow registerWindow = new RegisterWindow();
            registerWindow.ShowDialog();//导航到注册页面
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)//记住密码
        {
            bool rememberPassword = rememberPasswordCheckBox.IsChecked ?? false;
            string username = usernameTextBox.Text;
            string password = PasswordBox.Password;
            /*bool loginSuccess = AuthenticateUser(username, password);
            if(loginSucess)
             {
                if(rememberPassword)
                {
                    //存储密码
                }
                else
                {
                    //清除密码
                 }
             }*/
        }

    }
}
