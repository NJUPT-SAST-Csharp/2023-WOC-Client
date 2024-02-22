using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using SastWiki.Core.Contracts.InternalLink;
using SastWiki.Core.Models;
using SastWiki.Core.Services.Backend.Entry;
using SastWiki.Core.Services.InternalLink;
using SastWiki.WPF.Contracts;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SastWiki.WPF.ViewModels
{
    internal class EditPageVM : ObservableObject, INavigationAware
    {
        private readonly InternalLinkService _internalLinkService;
        private EntryProvider _entryProvider;

        Task<bool> INavigationAware.OnNavigatedFrom()
        {
            return Task.FromResult(true);
        }

        Task<bool> INavigationAware.OnNavigatedTo<T>(T parameters)
        {
            return Task.FromResult(true);
        }

        public string Title { get; set; }
        public string Content { get; set; }

        public ICommand SubmitCommand { get; }

        public EditPageVM()
        {
            _internalLinkService = new InternalLinkService();
            SubmitCommand = new RelayCommand(Submit);
        }

        private void Submit() 
        {
            _entryProvider.UpdateEntryAsync();
        }

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
