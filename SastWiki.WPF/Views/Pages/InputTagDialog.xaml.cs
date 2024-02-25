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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using CommunityToolkit.Mvvm.ComponentModel;

namespace SastWiki.WPF.Views.Pages
{
    /// <summary>
    /// InputTagDialog.xaml 的交互逻辑
    /// </summary>
    [ObservableObject]
    public partial class InputTagDialog : Window
    {
        public InputTagDialog()
        {
            InitializeComponent();
        }

        private void AddTagButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        public string GetText()
        {
            return TagTextBox.Text;
        }
    }
}
