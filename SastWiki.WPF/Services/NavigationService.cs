using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SastWiki.WPF;
using SastWiki.WPF.Contracts;

namespace SastWiki.WPF.Services
{
    /// <summary>
    /// 将MainWindow中的Frame抽象出来以便于实现链接跳转至其它词条等等导航功能的导航服务
    /// </summary>
    internal class NavigationService
    {
        private readonly MainWindow _mainWindow;

        public NavigationService(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
        }
    }
}
