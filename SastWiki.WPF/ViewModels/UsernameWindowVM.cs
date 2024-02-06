using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using Microsoft.Xaml.Behaviors;
using Prism.Commands;
using SastWiki.WPF;
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
    public class UsernameWindowVM : ObservableObject
    {
        private string? _username;
        private void Register()
        {
            RegisterWindow registerWindow = new();
            registerWindow.ShowDialog();
        }
        public ICommand RegisterCommand => new RelayCommand(Register);
        public ICommand LoginCommand => new RelayCommand(Login);
        public string Username
        {
            get { return _username; }
            set { SetProperty(ref _username, value); }
        }
        private void Login()
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

        private bool? rememberPassword;

        public bool? RememberPassword { get => rememberPassword; set => SetProperty(ref rememberPassword, value); }

        public UsernameWindowVM()
        {
            RememberPassword = false; 
            if (RememberPassword == true)
            {
                //保存至数据库
            }
            
        }
        private string newPassword;
        public string NewPassword
        {
            get { return newPassword; }
            set
            {
                if (newPassword != value)
                {
                    newPassword = value;
                    OnPropertyChanged(nameof(NewPassword));
                }
            }
        }
    }
    public static class PasswordBoxHelper
    {
        public static readonly DependencyProperty PasswordProperty =
            DependencyProperty.RegisterAttached("Password", typeof(string), typeof(PasswordBoxHelper), new PropertyMetadata(null, OnPasswordPropertyChanged));

        public static string GetPassword(DependencyObject obj)
        {
            return (string)obj.GetValue(PasswordProperty);
        }

        public static void SetPassword(DependencyObject obj, string value)
        {
            obj.SetValue(PasswordProperty, value);
        }

        private static void OnPasswordPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is PasswordBox passwordBox)
            {
                passwordBox.PasswordChanged -= PasswordBox_PasswordChanged;
                passwordBox.PasswordChanged += PasswordBox_PasswordChanged;
            }
        }

        private static void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (sender is PasswordBox passwordBox)
            {
                SetPassword(passwordBox, passwordBox.Password);
            }
        }
        
    }
}
