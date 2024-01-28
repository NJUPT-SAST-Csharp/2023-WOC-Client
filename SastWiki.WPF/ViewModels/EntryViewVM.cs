using CommunityToolkit.Mvvm.ComponentModel;
using SastWiki.WPF.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SastWiki.WPF.ViewModels
{
    public partial class EntryViewVM : ObservableObject, INavigationAware
    {
        [ObservableProperty]
        private string _url = string.Empty;

        bool INavigationAware.OnNavigatedFrom()
        {
            return true;
        }

        bool INavigationAware.OnNavigatedTo<T>(T parameters)
        {
            if (parameters is string url)
            {
                Url = url;
                return true;
            }
            return false;
        }
    }
}
