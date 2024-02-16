using SastWiki.WPF.Contracts;
using SastWiki.WPF.Views.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using SastWiki.WPF.ViewModels;

namespace SastWiki.WPF
{
    /// <summary>
    /// UsernameWIndow.xaml 的交互逻辑
    /// </summary>
    public partial class UsernameWindow : Window
    {
        public UsernameWindow()
        {
            InitializeComponent();
            DataContext = new UsernameWindowVM();
        }
    }
}
