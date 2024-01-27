using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SastWiki.WPF.Contracts
{
    public interface INavigationService
    {
        public bool NavigateTo(string pageKey, object? parameters = null);

        public bool NavigateBackward();

        public bool NavigateForward();
    }
}
