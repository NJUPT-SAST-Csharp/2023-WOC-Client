using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SastWiki.WPF.Contracts
{
    /// <summary>
    ///
    /// </summary>
    internal interface IPageService
    {
        Type GetPageType(string key);
    }
}
