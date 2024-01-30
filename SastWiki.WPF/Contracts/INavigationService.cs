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
        public Task<bool> NavigateTo<T>(Page page, T parameter);

        public Task<bool> NavigateTo(Page page);

        public Task<bool> NavigateBackward();

        public Task<bool> NavigateForward();
    }
}
