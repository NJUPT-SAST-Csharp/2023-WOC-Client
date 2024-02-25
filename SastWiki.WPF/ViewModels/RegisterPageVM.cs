using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Refit;
using SastWiki.Core.Contracts.User;

namespace SastWiki.WPF.ViewModels;

public class RegisterPageVM : ObservableObject
{
    public RegisterPageVM(IUserRegister _userRegister, IUserLogin _userLogin)
    {
        NewUsername = "";
        NewEmail = "";
        ConfirmPassword = "";
        userRegister = _userRegister;
        userLogin = _userLogin;
    }

    private readonly IUserRegister userRegister;
    private readonly IUserLogin userLogin;

    private string? newUsername;
    public string? NewUsername
    {
        get => newUsername;
        set => SetProperty(ref newUsername, value);
    }

    private string? newEmail;
    public string? NewEmail
    {
        get => newEmail;
        set => SetProperty(ref newEmail, value);
    }
    private RelayCommand? registerButton;
    public ICommand RegisterButton =>
        registerButton ??= new RelayCommand(async () =>
        {
            string username = NewUsername ?? string.Empty;
            string email = NewEmail ?? string.Empty;
            string password = ConfirmPassword ?? string.Empty;
            //注册使用UserRegister中的方法，~~存储数据使用SettingProvider的接口~~ Register了就行，UserRegister会自动存储数据

            if (username is not null && password is not null)
            {
                //使用UserRegister中的方法
                try
                {
                    await userRegister.RegisterAsync(username, email, password);
                    _ = MessageBox.Show($"注册成功");
                    //使用UserLogin中的方法
                    try
                    {
                        _ = await userLogin.LoginAsync(email, password);
                        _ = MessageBox.Show($"登录成功");
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

    private string? confirmPassword;
    public string? ConfirmPassword
    {
        get => confirmPassword;
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
        DependencyProperty.RegisterAttached(
            "CustomPassword",
            typeof(string),
            typeof(CustomPasswordBoxHelper),
            new PropertyMetadata(null, OnCustomPasswordPropertyChanged)
        );

    public static string? GetCustomPassword(DependencyObject obj) =>
        (string?)obj.GetValue(CustomPasswordProperty);

    public static void SetCustomPassword(DependencyObject obj, string value) =>
        obj.SetValue(CustomPasswordProperty, value);

    private static void OnCustomPasswordPropertyChanged(
        DependencyObject d,
        DependencyPropertyChangedEventArgs e
    )
    {
        if (d is PasswordBox passwordBox)
        {
            passwordBox.PasswordChanged -= CustomPasswordBox_PasswordChanged;
            if ((string)e.NewValue != passwordBox.Password)
            {
                passwordBox.Password = (string)e.NewValue;
            }

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
