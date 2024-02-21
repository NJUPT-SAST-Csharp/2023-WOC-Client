#pragma warning disable CS1998 // �첽����ȱ�� "await" �����������ͬ����ʽ����
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Web;
using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.Wpf;
using SastWiki.Core.Contracts.Backend.Entry;
using SastWiki.Core.Contracts.InternalLink;
using SastWiki.WPF.Contracts;
using SastWiki.WPF.Utils;

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
        readonly TaskCompletionSource<bool> _ensureWebviewInitialized = new();

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
            if (e.IsUserInitiated) // �ж��Ƿ�Ϊ�û���������ų���ͼƬ����
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
                        // ��Ĭ�������������
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
                _markdownProcessor.Output(
                    Markdown_text,
                    out string html_text,
                    out IEnumerable<int> images
                );

                // �����д��֮����õ�����������˵
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
            if (parameters is int id)
            {
                try
                {
                    Markdown_text = "Not Found";
                }
                catch (Exception)
                {
                    Markdown_text = "Not Found";
                    throw;
                }
                return true;
            }
            return false;
        }
    }
}

#pragma warning restore CS1998 // �첽����ȱ�� "await" �����������ͬ����ʽ����
