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
    public partial class SearchResultVM : ObservableObject, INavigationAware
    {
        [ObservableProperty]
        private string inquiryText;

        public SearchResultVM()
        {
            InquiryText = "123";
        }

        Task<bool> INavigationAware.OnNavigatedFrom()
        {
            return Task.FromResult(true);
        }

        Task<bool> INavigationAware.OnNavigatedTo<T>(T parameters)
        {
            // MessageBox.Show("OnNavigatedTo");
            if (parameters is string s)
            {
                InquiryText = s;
            }
            return Task.FromResult(true);
        }
    }
}
