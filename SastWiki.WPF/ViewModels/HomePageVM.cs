using CommunityToolkit.Mvvm.ComponentModel;
using SastWiki.WPF.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SastWiki.WPF.ViewModels
{
    internal class HomePageVM : ObservableObject, INavigationAware
    {
        bool INavigationAware.OnNavigatedFrom()
        {
            return true;
        }

        bool INavigationAware.OnNavigatedTo<T>(T parameters)
        {
            return true;
        }
    }
}
