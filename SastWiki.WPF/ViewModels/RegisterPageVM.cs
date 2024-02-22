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
    internal class RegisterPageVM : ObservableObject
    {
        private string? newUsername;
        public string? NewUsername { get => newUsername; set => SetProperty(ref newUsername, value); }

        private string? newEmail;
        public string? NewEmail { get => newEmail; set => SetProperty(ref newEmail, value); }
        private RelayCommand? registerButton;
        public ICommand RegisterButton => registerButton ??= new RelayCommand(PerformRegisterButton);
        private void PerformRegisterButton()
        {
            string username = NewUsername ?? string.Empty;
            string email = NewEmail ?? string.Empty;
            string password = ConfirmPassword ?? string.Empty;
            //注册使用UserRegister中的方法，存储数据使用SettingProvider的接口；
        }
        private string? confirmPassword;
        public string? ConfirmPassword
        {
            get { return confirmPassword; }
            set
            {
                confirmPassword = value;
                OnPropertyChanged(nameof(ConfirmPassword));
            }
        }
    }
    public static class CustomPasswordBoxHelper
    {
        public static readonly DependencyProperty CustomPasswordProperty =
            DependencyProperty.RegisterAttached("CustomPassword", typeof(string), typeof(CustomPasswordBoxHelper), new PropertyMetadata(null, OnCustomPasswordPropertyChanged));
        public static string? GetCustomPassword(DependencyObject obj)
        {
            return (string?)obj.GetValue(CustomPasswordProperty);
        }
        public static void SetCustomPassword(DependencyObject obj, string value)
        {
            obj.SetValue(CustomPasswordProperty, value);
        }
        private static void OnCustomPasswordPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is PasswordBox passwordBox)
            {
                passwordBox.PasswordChanged -= CustomPasswordBox_PasswordChanged;
                passwordBox.PasswordChanged += CustomPasswordBox_PasswordChanged;
            }
        }
        private static void CustomPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (sender is PasswordBox passwordBox)
            {
                SetCustomPassword(passwordBox, passwordBox.Password);
            }
        }
    }
}
