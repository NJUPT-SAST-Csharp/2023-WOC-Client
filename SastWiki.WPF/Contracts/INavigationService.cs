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
        public bool NavigateTo<T>(Page page, T parameter);

        public bool NavigateTo(Page page);

        public bool NavigateBackward();

        public bool NavigateForward();
    }
}
