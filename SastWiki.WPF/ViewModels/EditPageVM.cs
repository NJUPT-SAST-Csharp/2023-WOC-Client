using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using SastWiki.Core.Contracts.Backend.Entry;
using SastWiki.Core.Contracts.InternalLink;
using SastWiki.Core.Models;
using SastWiki.Core.Services.Backend.Entry;
using SastWiki.Core.Services.InternalLink;
using SastWiki.WPF.Contracts;

namespace SastWiki.WPF.ViewModels
{
    internal class EditPageVM : ObservableObject, INavigationAware
    {
        private EntryProvider _entryProvider;

        Task<bool> INavigationAware.OnNavigatedFrom()
        {
            return Task.FromResult(true);
        }

        async Task<bool> INavigationAware.OnNavigatedTo<T>(T parameters)
        {
            if (parameters is int id)
            {
                CurrentEntry = await _entryProvider.GetEntryByIdAsync(id);
                return true;
            }
            return false;
        }

        [ObservableProperty]
        private EntryDto _currentEntry;

        public ICommand SubmitCommand =>
            new RelayCommand(() =>
            {
                _entryProvider.UpdateEntryAsync();
            });

        public EditPageVM() { }

        public void AddImage()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                string imagePath = openFileDialog.FileName;

                Content += $"!Image";
            }
        }
    }
}
