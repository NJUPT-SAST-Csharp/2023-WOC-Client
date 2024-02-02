using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SastWiki.WPF.Contracts;
using SastWiki.WPF.Views.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SastWiki.WPF.ViewModels
{
    public partial class HomePageVM : ObservableObject, INavigationAware
    {
        [ObservableProperty]
        private string _markdown_text =
            @"
# Markdown Renderer Test

This is a **paragraph** !

---

## This is a heading 2

### This is a heading 3

#### This is a heading 4

##### This is a heading 5

This is a paragraph with a [external link](https://www.google.com) in it.

This is a paragraph with a [internal link](wiki://sast-wiki/Entry?id=114514) in it.

This is a List:
- Item 1

- Item 2

    - Sub Item

- Item 3

This is a numbered list:

1. Item 1

2. Item 2

3. Item 3
";

        private INavigationService _navigationService;

        public HomePageVM(INavigationService navigationSevice)
        {
            _navigationService = navigationSevice;
        }

        private void TestWebview2()
        {
            _navigationService.NavigateTo(App.GetService<EntryViewPage>(), Markdown_text);
        }

        public ICommand TestWebView2_Click => new RelayCommand(TestWebview2);

        Task<bool> INavigationAware.OnNavigatedFrom()
        {
            return Task.FromResult(true);
        }

        Task<bool> INavigationAware.OnNavigatedTo<T>(T parameters)
        {
            return Task.FromResult(true);
        }
    }
}
