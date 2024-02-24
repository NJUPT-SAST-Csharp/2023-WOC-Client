﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using Microsoft.Xaml.Behaviors;
using Prism.Commands;
using SastWiki.Core.Services.User;
using SastWiki.WPF;
using SastWiki.WPF.Contracts;
using SastWiki.WPF.Views.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SastWiki.WPF.ViewModels
{
    public class LoginPageVM : ObservableObject
    {
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
        public bool? RememberPassword { get => rememberPassword; set => SetProperty(ref rememberPassword, value); }

        public LoginPageVM()
        {
            RememberPassword = false;
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
        public object Current_Page { get => currentPage; set => SetProperty(ref currentPage, value); }

        private RelayCommand loginCommand;
        public ICommand LoginCommand => loginCommand ??= new RelayCommand(Login);
        private UserLogin _userLogin;

        public LoginPageVM(UserLogin userLogin)
        {
            _userLogin = userLogin;

            RememberPassword = false;
            if (RememberPassword == true)
            {
                string savedPassword = GetSavedPassword();

                if (!string.IsNullOrEmpty(savedPassword))
                {
                    NewPassword = savedPassword;
                }
            }
        }
        private async void Login()
        {
            string username = Username;
            string password = NewPassword;
            if (RememberPassword == true)
            {
                SavePassword(NewPassword);
            }
            try
            {
                var loggedInUser = await _userLogin.LoginAsync(username, password);

                if (loggedInUser != null)
                {
                    MessageBox.Show($"User {loggedInUser.Email} logged in successfully.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private RelayCommand registerCommand;
        public ICommand RegisterCommand => registerCommand ??= new RelayCommand(Register);
        private void Register()
        {
            RegisterPage registerPage = new();
            Current_Page = registerPage;
            RegisterVisibility = Visibility.Collapsed;
        }
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
            DependencyProperty.RegisterAttached("Password", typeof(string), typeof(PasswordBoxHelper), new PropertyMetadata(null, OnPasswordPropertyChanged));

        public static string? GetPassword(DependencyObject obj)
        {
            return (string?)obj.GetValue(PasswordProperty);
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