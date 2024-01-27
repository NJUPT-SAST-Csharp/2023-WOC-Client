using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using SastWiki.WPF;
using SastWiki.WPF.Contracts;

namespace SastWiki.WPF.Services
{
    /// <summary>
    /// 将MainWindow中的Frame抽象出来以便于实现链接跳转至其它词条等等导航功能的导航服务
    /// </summary>
    internal class NavigationService : INavigationService
    {
        private Frame? _frame;
        private readonly IPageService _pageService;
        private object? _currentParameters;
        public Frame? Frame
        {
            get
            {
                if (_frame == null)
                {
                    _frame = App.GetService<MainWindow>().ContentFrame;
                    _frame.Navigated += OnNavigated;
                }

                return _frame;
            }
            set { _frame = value; }
        }

        public NavigationService(IPageService pageService)
        {
            _pageService = pageService;
        }

        bool INavigationService.NavigateBackward()
        {
            throw new NotImplementedException();
        }

        bool INavigationService.NavigateForward()
        {
            throw new NotImplementedException();
        }

        bool INavigationService.NavigateTo(string pageKey, object? parameter)
        {
            var pageType = _pageService.GetPageType(pageKey); // 从PageService中获取要跳转的页面的类型

            if (
                Frame is Frame frame
                && (
                    frame.Content?.GetType() != pageType // 如果当前页面与要跳转的页面不是同一个页面
                    || (parameter != null && !parameter.Equals(_currentParameters)) // 或者当前页面的参数与要跳转的参数不相同
                )
            )
            {
                var vmBeforeNavigation = frame.Content
                    ?.GetType()
                    .GetProperty("DataContext")
                    ?.GetValue(frame.Content, null); // 借助反射获取跳转前页面的ViewModel，接下来要调用其OnNavigatedFrom方法
                var navigated = frame.Navigate(App.GetService(pageType), parameter); // 则跳转

                if (navigated)
                {
                    _currentParameters = parameter;
                    if (vmBeforeNavigation is INavigationAware navigationAware)
                    {
                        navigationAware.OnNavigatedFrom();
                    }
                }

                return navigated;
            }

            return false;
        }

        private void OnNavigated(object sender, NavigationEventArgs e)
        {
            if (sender is Frame frame)
            {
                if (
                    frame.Content
                        ?.GetType()
                        .GetProperty("DataContext")
                        ?.GetValue(frame.Content, null)
                    is INavigationAware navigationAware
                )
                {
                    navigationAware.OnNavigatedTo(e.ExtraData);
                }

                // Navigated?.Invoke(sender, e);
            }
        }
    }
}
