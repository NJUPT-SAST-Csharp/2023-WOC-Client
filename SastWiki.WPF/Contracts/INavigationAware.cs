using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SastWiki.WPF.Contracts
{
    public interface INavigationAware
    {
        public Task<bool> OnNavigatedTo<T>(T parameters);
        public Task<bool> OnNavigatedFrom();
    }
}
