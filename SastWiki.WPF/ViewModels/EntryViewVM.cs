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
        private string _markdown_text = String.Empty;

        Task<bool> INavigationAware.OnNavigatedFrom()
        {
            return Task.FromResult(true);
        }

        Task<bool> INavigationAware.OnNavigatedTo<T>(T parameters)
        {
            if (parameters is string markdown_text)
            {
                Markdown_text = markdown_text;
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }
    }
}
