using System;
using System.Collections.Generic;
using System.Data.Common;
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
        private object? _currentParameters;
        private INavigationAware? _currentVM;
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

        public NavigationService() { }

        bool INavigationService.NavigateBackward()
        {
            throw new NotImplementedException();
        }

        bool INavigationService.NavigateForward()
        {
            throw new NotImplementedException();
        }

        public bool NavigateTo(Page page)
        {
            if (
                Frame is Frame frame && _currentVM?.GetType() != page.GetType() // 应该避免不了了，开摆
            )
            {
                var navigated = frame.Navigate(page);

                if (navigated)
                {
                    if (_currentVM is INavigationAware old_viewmodel)
                    {
                        old_viewmodel.OnNavigatedFrom();
                    }
                    _currentParameters = null;
                    if (page.DataContext is INavigationAware new_viewmodel)
                    {
                        _currentVM = new_viewmodel;
                    }
                }

                return navigated;
            }
            return false;
        }

        public bool NavigateTo<T>(Page page, T parameter)
        {
            if (
                Frame is Frame frame
                && (
                    _currentVM?.GetType() != page.GetType()
                    || parameter != null && !parameter.Equals(_currentParameters)
                )
            )
            {
                var navigated = frame.Navigate(page, parameter);

                if (navigated)
                {
                    if (_currentVM is INavigationAware old_viewmodel)
                    {
                        old_viewmodel.OnNavigatedFrom();
                    }
                    _currentParameters = parameter;
                    if (page.DataContext is INavigationAware new_viewmodel)
                    {
                        _currentVM = new_viewmodel;
                    }
                }

                return navigated;
            }

            return false;
        }

        private void OnNavigated(object sender, NavigationEventArgs e)
        {
            if (sender is Frame frame && _currentVM is INavigationAware navigationAware)
            {
                navigationAware.OnNavigatedTo(e.ExtraData);
            }
        }
    }
}
