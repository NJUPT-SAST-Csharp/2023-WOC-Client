using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Refit;
using SastWiki.Core.Contracts.Infrastructure.SettingsService;
using SastWiki.Core.Contracts.User;
using SastWiki.WPF.Views.Pages;

namespace SastWiki.WPF.ViewModels;

public class LoginPageVM : ObservableObject
{
    private readonly IUserLogin userLogin;
    private readonly ISettingsProvider settingsProvider;

    private string? _username;
    public string? Username
    {
        get => _username;
        set => SetProperty(ref _username, value);
    }
    private string? newPassword;
    public string? NewPassword
    {
        get => newPassword;
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

    public LoginPageVM(IUserLogin _userLogin, ISettingsProvider _settingsProvider)
    {
        userLogin = _userLogin;
        settingsProvider = _settingsProvider;
        RememberPassword = false;
        NewPassword = ""; // 这是一个fix，用来激活OnPasswordPropertyChanged()方法，以绑定Password的更新事件
        _ = GetSavedPassword();
    }

    private async Task GetSavedPassword()
    {
        string savedPassword;
        string savedUsername;
        try
        {
            savedPassword = await settingsProvider.GetItem<string>("SavedPassword") ?? string.Empty;
            savedUsername = await settingsProvider.GetItem<string>("SavedUsername") ?? string.Empty;
            RememberPassword = true;
        }
        catch (Exception)
        {
            savedPassword = string.Empty;
            savedUsername = string.Empty;
        }

        // MessageBox.Show(savedPassword);
        // MessageBox.Show(savedUsername);
        Username = savedUsername;
        NewPassword = savedPassword;
    }

    private async Task SavePassword(string username, string password)
    {
        await settingsProvider.SetItem("SavedPassword", password);
        await settingsProvider.SetItem("SavedUsername", username);
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
                    await SavePassword(username, password);
                }
                //使用UserLogin中的方法
                try
                {
                    _ = await userLogin.LoginAsync(username, password);
                    _ = MessageBox.Show("登录成功");
                }
                catch (ApiException e)
                {
                    _ = MessageBox.Show(e.Message, e.Content);
                }
                catch (Exception e)
                {
                    _ = MessageBox.Show(e.Message);
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

    public static string? GetPassword(DependencyObject obj) =>
        (string?)obj.GetValue(PasswordProperty);

    public static void SetPassword(DependencyObject obj, string value) =>
        obj.SetValue(PasswordProperty, value);

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
