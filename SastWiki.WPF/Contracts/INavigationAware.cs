using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SastWiki.WPF.Contracts
{
    internal interface INavigationAware
    {
        public bool OnNavigatedTo(object? parameters = null);
        public bool OnNavigatedFrom();
    }
}
