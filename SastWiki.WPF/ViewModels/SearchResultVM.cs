using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using SastWiki.WPF.Contracts;

namespace SastWiki.WPF.ViewModels
{
    public partial class SearchResultVM : ObservableObject, INavigationAware
    {
        [ObservableProperty]
        private string _searchText;

        public SearchResultVM()
        {
            SearchText = "";
        }

        Task<bool> INavigationAware.OnNavigatedFrom()
        {
            return Task.FromResult(true);
        }

        Task<bool> INavigationAware.OnNavigatedTo<T>(T parameters)
        {
            if (parameters is string s)
            {
                SearchText = s;
            }
            return Task.FromResult(true);
        }
    }
}
