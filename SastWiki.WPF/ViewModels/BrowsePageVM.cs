using CommunityToolkit.Mvvm.ComponentModel;
using SastWiki.WPF.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using SastWiki.Core.Contracts.Backend.Tag;

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

    public class TagEntry
    {
        public string TagName
        {
            get; set;
        }
        public IEnumerable<int> Ids
        {
            get; set;
        }
    }
}
