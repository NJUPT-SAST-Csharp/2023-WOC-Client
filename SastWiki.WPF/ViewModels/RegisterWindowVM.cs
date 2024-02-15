using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;

namespace SastWiki.WPF.ViewModels
{
    public class RegisterWindowVM : ObservableObject
    {

        private string? newUsername;
        public string? NewUsername { get => newUsername; set => SetProperty(ref newUsername, value); }

        private RelayCommand? registerButton;
        public ICommand RegisterButton => registerButton ??= new RelayCommand(PerformRegisterButton);

        private void PerformRegisterButton()
        {
            //存储用户信息；
        }
        private string? _newPassword;
        public string? NewPassword
        {
            get { return _newPassword; }
            set
            {
                _newPassword = value;
                OnPropertyChanged(nameof(NewPassword));
            }
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
