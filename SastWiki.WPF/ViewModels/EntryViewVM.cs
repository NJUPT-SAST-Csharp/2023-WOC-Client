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

        Task<bool> INavigationAware.OnNavigatedFrom()
        {
            return Task.FromResult(true);
        }

        Task<bool> INavigationAware.OnNavigatedTo<T>(T parameters)
        {
            if (parameters is string url)
            {
                Url = url;
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }
    }
}
