using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Web;
using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.Wpf;
using Refit;
using SastWiki.Core.Contracts.Backend.Entry;
using SastWiki.Core.Contracts.InternalLink;
using SastWiki.Core.Models.Dto;
using SastWiki.WPF.Contracts;
using SastWiki.WPF.Utils;

namespace SastWiki.WPF.ViewModels
{
    public partial class EntryViewVM(
        IMarkdownProcessor markdownProcessor,
        IEntryProvider entryProvider
    ) : ObservableObject, INavigationAware
    {
        private WebView2? _webview;
        public WebView2? WebView
        {
            private get => _webview;
            set
            {
                if (value is not null)
                {
                    _webview = value;
                    _webview.NavigationStarting += WebView_NavigationStarting;
                    PropertyChanged += LoadMarkdownDoc;
                    _webview
                        .EnsureCoreWebView2Async()
                        .ContinueWith(
                            (_) =>
                            {
                                _ensureWebviewInitialized.SetResult(true);
                            }
                        );
                }
            }
        }

        readonly TaskCompletionSource<bool> _ensureWebviewInitialized = new();

        public ICommand RefreshCommand =>
            new RelayCommand(
                async () =>
                {
                    try
                    {
                        await LoadPage(Id);
                    }
                    catch (ApiException e)
                    {
                        MessageBox.Show(e.Message, e.Content);
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message);
                    }
                },
                () => IsLoaded
            );

        [ObservableProperty]
        private string _markdown_text = String.Empty;

        [ObservableProperty]
        private string _title = String.Empty;

        [ObservableProperty]
        private int _id = -1;

        [ObservableProperty]
        private EntryDto _currentEntry = new();

        [ObservableProperty]
        private bool isLoaded = false;

        private void WebView_NavigationStarting(
            object? sender,
            CoreWebView2NavigationStartingEventArgs e
        )
        {
            if (e.IsUserInitiated) // 判断是否为用户点击，以排除掉图片请求
            {
                IInternalLinkValidator validator = App.GetService<IInternalLinkValidator>();
                if (Uri.TryCreate(e.Uri, UriKind.Absolute, out Uri? result))
                    if (validator.Validate(result))
                    {
                        IInternalLinkHandler handler = App.GetService<IInternalLinkHandler>();
                        handler.Trigger(result);
                        e.Cancel = true;
                    }
                    else
                    {
                        // 从默认浏览器打开链接
                        System.Diagnostics.Process.Start("explorer.exe", e.Uri);
                        e.Cancel = true;
                    }
            }
        }

        async void LoadMarkdownDoc(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Markdown_text))
            {
                // string css_text;
                markdownProcessor.Output(
                    Markdown_text,
                    out string html_text,
                    out IEnumerable<int> images
                );

                // 这两行大概之后会用到，先留着再说
                // MessageBox.Show(html_text);
                // MessageBox.Show(Markdown_text);

                await _ensureWebviewInitialized.Task;
                WebView!.NavigateToString(html_text);
            }
        }

        async Task LoadPage(int id)
        {
            Title = "Loading...";
            Id = id;
            try
            {
                var entry = await entryProvider.GetEntryByIdAsync(id);
                Markdown_text = entry.Content ?? "# ERROR";
                CurrentEntry = entry;
            }
            catch (Exception)
            {
                Markdown_text = "# ERROR";
                IsLoaded = false;
                CurrentEntry = new();
                throw;
            }

            IsLoaded = true;

            Title = (await entryProvider.GetEntryByIdAsync(id)).Title ?? "No Title";

            OnPropertyChanged(nameof(RefreshCommand));
        }

        Task<bool> INavigationAware.OnNavigatedFrom()
        {
            return Task.FromResult(true);
        }

        async Task<bool> INavigationAware.OnNavigatedTo<T>(T parameters)
        {
            if (parameters is int id)
            {
                try
                {
                    await LoadPage(id);
                }
                catch (ApiException e)
                {
                    MessageBox.Show(e.Message, e.Content);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }

                return true;
            }
            return false;
        }
    }
}
