using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SastWiki.WPF.Contracts
{
    public interface INavigationService
    {
        /// <summary>
        /// 导航到指定页面并传递指定参数。需要目标页面实现<seealso cref="INavigationAware"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="page">要跳转到的页面实例，你可以从DI容器里面获取一个</param>
        /// <param name="parameter">你要传递给要跳转到的页面的参数，在目标页面的<see cref="INavigationAware.OnNavigatedTo{T}(T)"/>中进一步处理</param>
        /// <returns>是否跳转成功</returns>
        public Task<bool> NavigateTo<T>(Page page, T parameter);

        /// <summary>
        /// 导航到指定页面，不需要目标页面实现<seealso cref="INavigationAware"/>，因而也不能传递参数。
        /// </summary>
        /// <param name="page">要跳转到的页面实例，你可以从DI容器里面获取一个</param>
        /// <returns>是否跳转成功</returns>
        public Task<bool> NavigateTo(Page page);

        public Task<bool> NavigateBackward();

        public Task<bool> NavigateForward();
    }
}
