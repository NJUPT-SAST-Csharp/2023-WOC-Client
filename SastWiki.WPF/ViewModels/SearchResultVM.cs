using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SastWiki.Core.Models.Dto;
using SastWiki.WPF.Contracts;

namespace SastWiki.WPF.ViewModels
{
    public partial class SearchResultVM : ObservableObject, INavigationAware
    {
        [ObservableProperty]
        private string _searchText;

        [ObservableProperty]
        private List<EntryDto> _searchResult =
        [
            new()
            {
                Id = 1,
                Title = "Test",
                Content = "Testsafdafaf",
                CategoryName = "SAST",
                TagNames = ["2024", "Feburary"]
            },
            new()
            {
                Id = 2,
                Title = "Sast Wiki Test",
                Content = "Testsafdafaf",
                CategoryName = "NJUPT",
                TagNames = ["2023", "January"]
            }
        ];

        public SearchResultVM()
        {
            SearchText = "";
        }

        public ICommand TagClickCommand => new RelayCommand<string>(TagClick);

        private void TagClick(string? tag)
        {
            MessageBox.Show($"Tag Clicked! {tag}");
            // NavigationService.NavigateTo("SearchResult", tag);
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
