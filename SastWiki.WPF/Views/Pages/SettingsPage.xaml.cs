﻿using Jamesnet.Wpf.Controls;
using SastWiki.WPF.Contracts;
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
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static SastWiki.WPF.Utils.SystemBackdrop.PInvoke.ParameterTypes;

namespace SastWiki.WPF.Views.Pages
{
    /// <summary>
    /// SettingsPage.xaml 的交互逻辑
    /// </summary>
    public partial class SettingsPage : Page
    {
        public SettingsPage()
        {
            InitializeComponent();
        }

        private void IndividuationPageTurn(object sender, EventArgs e)
        {
            Page ThemeChangePage = new ThemeChangePage();
            Page_Change.Content = new Frame() { Content = ThemeChangePage, };
        }
    }
}
