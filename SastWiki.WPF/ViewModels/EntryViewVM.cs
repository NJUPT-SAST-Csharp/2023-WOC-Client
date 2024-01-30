using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Web;
using Microsoft.Web.WebView2.Wpf;
using SastWiki.WPF.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SastWiki.WPF.ViewModels
{
    public partial class EntryViewVM : ObservableObject, INavigationAware
    {
        private IMarkdownProcessor _markdownProcessor;

        public WebView2? WebView { private get; set; }

        [ObservableProperty]
        private string _markdown_text = String.Empty;

        public EntryViewVM(IMarkdownProcessor markdownProcessor)
        {
            _markdownProcessor = markdownProcessor;
            PropertyChanged += LoadMarkdownDoc;
        }

        async void LoadMarkdownDoc(object? sender, PropertyChangedEventArgs e)
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
            await WebView.EnsureCoreWebView2Async(); // TODO: 到时候处理一下这个null（可能写个事件之类的？）
            WebView.NavigateToString(html_text);
        }

        Task<bool> INavigationAware.OnNavigatedFrom()
        {
            return Task.FromResult(true);
        }

#pragma warning disable CS1998 // 异步方法缺少 "await" 运算符，将以同步方式运行
        async Task<bool> INavigationAware.OnNavigatedTo<T>(T parameters)
#pragma warning restore CS1998 // 异步方法缺少 "await" 运算符，将以同步方式运行
        {
            if (parameters is string markdown_text)
            {
                Markdown_text = markdown_text;
                return true;
            }
            return false;
        }
    }
}
