#pragma warning disable CS1998 // 异步方法缺少 "await" 运算符，将以同步方式运行
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Web;
using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.Wpf;
using SastWiki.Core.Models;
using SastWiki.WPF.Contracts;
using SastWiki.WPF.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Web;

namespace SastWiki.WPF.ViewModels
{
    public partial class EntryViewVM : ObservableObject, INavigationAware
    {
        private IMarkdownProcessor _markdownProcessor;

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
        TaskCompletionSource<bool> _ensureWebviewInitialized = new();

        [ObservableProperty]
        private string _markdown_text = String.Empty;

        public EntryViewVM(IMarkdownProcessor markdownProcessor)
        {
            _markdownProcessor = markdownProcessor;
        }

        private void WebView_NavigationStarting(
            object? sender,
            CoreWebView2NavigationStartingEventArgs e
        )
        {
            if (e.IsUserInitiated)
            {
                if (InternalLink.TryParse(e.Uri))
                {
                    InternalLink ilink = new(e.Uri);
                    MessageBox.Show(
                        $"Internal Link! Path is {ilink.Path}, Query is {System.Web.HttpUtility.UrlDecode(ilink.Query[1..])}" // TODO: 越界警告
                    );
                    e.Cancel = true;
                }
            }
        }

        async void LoadMarkdownDoc(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Markdown_text))
            {
                // string css_text;
                _markdownProcessor.Output(
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

        Task<bool> INavigationAware.OnNavigatedFrom()
        {
            return Task.FromResult(true);
        }

        async Task<bool> INavigationAware.OnNavigatedTo<T>(T parameters)
        {
            if (parameters is string markdown_text)
            {
                Markdown_text = markdown_text;
                return true;
            }
            if (parameters is Uri internal_uri)
            {
                return true;
            }
            return false;
        }
    }
}

#pragma warning restore CS1998 // 异步方法缺少 "await" 运算符，将以同步方式运行
