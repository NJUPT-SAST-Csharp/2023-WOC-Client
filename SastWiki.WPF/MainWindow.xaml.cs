using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using SastWiki.WPF.Contracts;
using SastWiki.WPF.ViewModels;
using SastWiki.WPF.Views.Pages;
using static SastWiki.WPF.Utils.SystemBackdrop.PInvoke.Methods;
using static SastWiki.WPF.Utils.SystemBackdrop.PInvoke.ParameterTypes;

namespace SastWiki.WPF;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private readonly INavigationService _navigationService;

    public MainWindow(MainWindowVM mainWindowVM, INavigationService navigationService)
    {
        DataContext = mainWindowVM;
        _navigationService = navigationService;
        InitializeComponent();
        _ = ContentFrame.Navigate(App.GetService<HomePage>());
    }

    public bool IsDark { get; set; } = false;

    public int CurrentPage { get; private set; } = 0;

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        IntPtr mainWindowPtr = new WindowInteropHelper(this).Handle;
        var mainWindowSrc = HwndSource.FromHwnd(mainWindowPtr);
        mainWindowSrc.CompositionTarget.BackgroundColor = Color.FromArgb(0, 0, 0, 0);

        MARGINS margins =
            new()
            {
                cxLeftWidth = -1,
                cxRightWidth = -1,
                cyTopHeight = -1,
                cyBottomHeight = -1
            };

        _ = ExtendFrame(mainWindowSrc.Handle, margins);
        _ = SetWindowAttribute(
            new WindowInteropHelper(this).Handle,
            DWMWINDOWATTRIBUTE.DWMWA_SYSTEMBACKDROP_TYPE,
            2
        );

        RefreshDarkMode();
    }

    public void RefreshDarkMode()
    {
        int flag = IsDark ? 1 : 0;
        _ = SetWindowAttribute(
            new WindowInteropHelper(this).Handle,
            DWMWINDOWATTRIBUTE.DWMWA_USE_IMMERSIVE_DARK_MODE,
            flag
        );
    }

    private void SearchBox_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
    {
        if (e.Key == Key.Enter)
        {
            (DataContext as MainWindowVM)?.GoToSearchResultPageCommand.Execute(null);
        }
    }
}
