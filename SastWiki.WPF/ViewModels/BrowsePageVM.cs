using CommunityToolkit.Mvvm.ComponentModel;
using SastWiki.WPF.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SastWiki.WPF.ViewModels
{
    internal class BrowsePageVM : ObservableObject, INavigationAware
    {
        bool INavigationAware.OnNavigatedFrom()
        {
            return true;
        }

        bool INavigationAware.OnNavigatedTo(object? parameters)
        {
            MessageBox.Show("OnNavigatedTo");
            return true;
        }
    }
}
