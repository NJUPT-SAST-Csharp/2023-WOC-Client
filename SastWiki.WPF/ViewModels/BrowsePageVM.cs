using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using SastWiki.Core.Contracts.Backend.Tag;
using SastWiki.WPF.Contracts;

namespace SastWiki.WPF.ViewModels
{
    internal class BrowsePageVM : ObservableObject, INavigationAware
    {
        Task<bool> INavigationAware.OnNavigatedFrom()
        {
            return Task.FromResult(true);
        }

        Task<bool> INavigationAware.OnNavigatedTo<T>(T parameters)
        {
            MessageBox.Show("OnNavigatedTo");
            return Task.FromResult(true);
        }
    }

    public record TagEntry
    {
        public required string TagName { get; set; }
        public required IEnumerable<int> Ids { get; set; }
    }
}
