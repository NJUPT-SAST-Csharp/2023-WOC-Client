using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Refit;
using SastWiki.Core.Contracts.Backend.Entry;
using SastWiki.Core.Contracts.Infrastructure.CacheService;
using SastWiki.Core.Contracts.Infrastructure.SettingsService;
using SastWiki.WPF.Contracts;
using SastWiki.WPF.Views.Pages;

namespace SastWiki.WPF.ViewModels
{
    public partial class HomePageVM(
        INavigationService navigationService,
        IEntryProvider entryProvider
    ) : ObservableObject, INavigationAware
    {
        public ICommand RandomEntryCommand =>
            new RelayCommand(async () =>
            {
                try
                {
                    var idList = (await entryProvider.GetEntryMetadataList())
                        .Select(x => x.Id ?? 0)
                        .Where(x => x != 0)
                        .ToList();
                    await navigationService.NavigateTo<int>(
                        App.GetService<EntryViewPage>(),
                        (int)idList[new Random().Next(0, idList.Count)]
                    );
                }
                catch (ApiException e)
                {
                    MessageBox.Show(e.Message, e.Content);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            });

        public ICommand LatestChangedEntryCommand =>
            new RelayCommand(async () =>
            {
                try
                {
                    var id = (await entryProvider.GetEntryMetadataList())
                        .Select(x => x.Id ?? 0)
                        .Where(x => x != 0)
                        .Max();
                    await navigationService.NavigateTo<int>(App.GetService<EntryViewPage>(), id);
                }
                catch (ApiException e)
                {
                    MessageBox.Show(e.Message, e.Content);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            });

        Task<bool> INavigationAware.OnNavigatedFrom()
        {
            return Task.FromResult(true);
        }

        Task<bool> INavigationAware.OnNavigatedTo<T>(T parameters)
        {
            return Task.FromResult(true);
        }
    }
}
