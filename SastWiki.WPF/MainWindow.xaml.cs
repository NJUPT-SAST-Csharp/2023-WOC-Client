using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media;
using static SastWiki.WPF.Utils.SystemBackdrop.PInvoke.ParameterTypes;
using static SastWiki.WPF.Utils.SystemBackdrop.PInvoke.Methods;
using SastWiki.WPF.Views.Pages;
using Microsoft.Extensions.Hosting;
using SastWiki.WPF.ViewModels;
using SastWiki.WPF.Contracts;
using System.Windows.Input;

namespace SastWiki.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindowVM VM { get; }
        private INavigationService _navigationService;

        public MainWindow(MainWindowVM mainWindowVM, INavigationService navigationService)
        {
            VM = mainWindowVM;
            _navigationService = navigationService;
            InitializeComponent();
        }

        public bool IsDark { get; private set; } = false;

        public int CurrentPage { get; private set; } = 0;

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            IntPtr mainWindowPtr = new WindowInteropHelper(this).Handle;
            HwndSource mainWindowSrc = HwndSource.FromHwnd(mainWindowPtr);
            mainWindowSrc.CompositionTarget.BackgroundColor = Color.FromArgb(0, 0, 0, 0);

            MARGINS margins = new MARGINS();
            margins.cxLeftWidth = -1;
            margins.cxRightWidth = -1;
            margins.cyTopHeight = -1;
            margins.cyBottomHeight = -1;

            ExtendFrame(mainWindowSrc.Handle, margins);
            SetWindowAttribute(
                new WindowInteropHelper(this).Handle,
                DWMWINDOWATTRIBUTE.DWMWA_SYSTEMBACKDROP_TYPE,
                2
            );

            RefreshDarkMode();
        }

        private void RefreshDarkMode()
        {
            int flag = IsDark ? 1 : 0;
            SetWindowAttribute(
                new WindowInteropHelper(this).Handle,
                DWMWINDOWATTRIBUTE.DWMWA_USE_IMMERSIVE_DARK_MODE,
                flag
            );
        }

        private void NavigateTo_HomePage(object sender, RoutedEventArgs e) =>
            _navigationService.NavigateTo(typeof(HomePageVM).FullName!);

        private void NavigateTo_BrowsePage(object sender, RoutedEventArgs e) =>
            _navigationService.NavigateTo(typeof(BrowsePageVM).FullName!);

        private void NavigateTo_SettingsPage(object sender, RoutedEventArgs e) =>
            _navigationService.NavigateTo(typeof(SettingsVM).FullName!);

        private void SearchBox_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                _navigationService.NavigateTo(typeof(SearchResultVM).FullName!, SearchBox.Text);
                return;
            }
        }
    }
}
