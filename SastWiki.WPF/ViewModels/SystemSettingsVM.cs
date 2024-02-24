using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SastWiki.Core.Contracts.Infrastructure.CacheService;

namespace SastWiki.WPF.ViewModels
{
    public class SystemSettingsVM(ICacheStorage cacheStorage) : ObservableObject
    {
        public ICommand ClearAllCacheCommand =>
            new RelayCommand(async () =>
            {
                await cacheStorage.ForceClearCacheAsync();
            });
    }
}
