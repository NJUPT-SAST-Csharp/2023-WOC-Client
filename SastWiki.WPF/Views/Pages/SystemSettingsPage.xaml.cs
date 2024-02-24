﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SastWiki.WPF.ViewModels;

namespace SastWiki.WPF.Views.Pages
{
    /// <summary>
    /// SystemSettingsPage.xaml 的交互逻辑
    /// </summary>
    public partial class SystemSettingsPage : Page
    {
        public SystemSettingsPage(SystemSettingsVM vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}
