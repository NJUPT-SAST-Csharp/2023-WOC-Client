using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SastWiki.WPF.Contracts
{
    /// <summary>
    ///
    /// </summary>
    internal interface IPageService
    {
        Type GetPageType(string key);

        void Configure<VM, V>()
            where VM : ObservableObject
            where V : Page;
    }
}
