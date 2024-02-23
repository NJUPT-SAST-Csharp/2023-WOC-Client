using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SastWiki.Core.Contracts.Backend.Entry;
using SastWiki.Core.Models.Dto;
using SastWiki.WPF.Contracts;
using SastWiki.WPF.Views.Pages;

namespace SastWiki.WPF.ViewModels
{
    public partial class SearchResultVM(
        IEntryProvider entryProvider,
        INavigationService navigationService
    ) : ObservableObject, INavigationAware
    {
        [ObservableProperty]
        private string _searchText = "";

        [ObservableProperty]
        private List<EntryDto> _searchResult = [];

        public ICommand TagClickCommand => new RelayCommand<string>(TagClick);

        private void TagClick(string? tag)
        {
            //MessageBox.Show($"Tag Clicked! {tag}");
            navigationService.NavigateTo(App.GetService<SearchResultPage>(), tag);
        }

        public async Task Search(string s)
        {
            SearchText = s;
            var metadatalist = await entryProvider.GetEntryMetadataList();
            SearchResult = metadatalist
                .Where(x =>
                    (x.Title ?? "") == (s)
                    || x.TagNames.Where(x => x == (s)).Any()
                    || (x.CategoryName ?? "") == (s)
                )
                .Concat(
                    metadatalist.Where(x =>
                        (x.Title ?? "").Contains(s)
                        || x.TagNames.Where(x => x.Contains(s)).Any()
                        || (x.CategoryName ?? "").Contains(s)
                    )
                )
                .DistinctBy(x => x.Id)
                .ToList();
        }

        Task<bool> INavigationAware.OnNavigatedFrom()
        {
            return Task.FromResult(true);
        }

        async Task<bool> INavigationAware.OnNavigatedTo<T>(T parameters)
        {
            if (parameters is string s)
            {
                SearchText = s;
                await Search(s);
            }
            return true;
        }
    }
}
