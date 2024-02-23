using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using Microsoft.Xaml.Behaviors;
using Prism.Commands;
using Refit;
using SastWiki.Core.Contracts.User;
using SastWiki.WPF;
using SastWiki.WPF.Contracts;
using SastWiki.WPF.Views.Pages;

namespace SastWiki.WPF.ViewModels
{
    public class LoginPageVM : ObservableObject
    {
        IUserLogin userLogin;

        private string? _username;
        public string? Username
        {
            get { return _username; }
            set { SetProperty(ref _username, value); }
        }
        private string? newPassword;
        public string? NewPassword
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
        private bool? rememberPassword;
        public bool? RememberPassword
        {
            get => rememberPassword;
            set => SetProperty(ref rememberPassword, value);
        }

        public LoginPageVM(IUserLogin _userLogin)
        {
            userLogin = _userLogin;
            RememberPassword = false;
            NewPassword = ""; // 这是一个fix，用来激活OnPasswordPropertyChanged()方法，以绑定Password的更新事件
            if (RememberPassword == true)
            {
                string savedPassword = GetSavedPassword();

                if (!string.IsNullOrEmpty(savedPassword))
                {
                    NewPassword = savedPassword;
                }
            }
        }

        private string GetSavedPassword()
        {
            string savedPassword = string.Empty;

            if (Application.Current.Properties.Contains("SavedPassword"))
            {
                savedPassword = Application.Current.Properties["SavedPassword"].ToString();
            }

            return savedPassword;
        }

        private void SavePassword(string password)
        {
            Application.Current.Properties["SavedPassword"] = password;
        }

        private object currentPage;
        public object Current_Page
        {
            get => currentPage;
            set => SetProperty(ref currentPage, value);
        }

        public ICommand LoginCommand =>
            new RelayCommand(async () =>
            {
                // OnPropertyChanged(nameof(NewPassword));
                string? username = Username;
                string? password = NewPassword;
                if (username is not null && password is not null)
                {
                    if (RememberPassword == true)
                    {
                        SavePassword(password);
                    }
                    //使用UserLogin中的方法
                    try
                    {
                        await userLogin.LoginAsync(username, password);
                        MessageBox.Show("登录成功");
                    }
                    catch (ApiException e)
                    {
                        MessageBox.Show(e.Message, e.Content);
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message);
                    }
                }
            });

        public ICommand RegisterCommand =>
            new RelayCommand(() =>
            {
                RegisterPage registerPage = App.GetService<RegisterPage>();
                Current_Page = registerPage;
                RegisterVisibility = Visibility.Collapsed;
            });

        private Visibility registerVisibility = Visibility.Visible;
        public Visibility RegisterVisibility
        {
            get => registerVisibility;
            set => SetProperty(ref registerVisibility, value);
        }
    }

    public static class PasswordBoxHelper
    {
        public static readonly DependencyProperty PasswordProperty =
            DependencyProperty.RegisterAttached(
                "Password",
                typeof(string),
                typeof(PasswordBoxHelper),
                new PropertyMetadata(null, OnPasswordPropertyChanged)
            );

        public static string? GetPassword(DependencyObject obj)
        {
            return (string?)obj.GetValue(PasswordProperty);
        }

        public static void SetPassword(DependencyObject obj, string value)
        {
            obj.SetValue(PasswordProperty, value);
        }

        private static void OnPasswordPropertyChanged(
            DependencyObject d,
            DependencyPropertyChangedEventArgs e
        )
        {
            if (d is PasswordBox passwordBox)
            {
                passwordBox.PasswordChanged -= PasswordBox_PasswordChanged;
                if ((string)e.NewValue != passwordBox.Password)
                {
                    passwordBox.Password = (string)e.NewValue;
                }
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
