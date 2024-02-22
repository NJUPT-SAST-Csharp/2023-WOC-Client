using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using Refit;
using SastWiki.Core.Contracts.Backend.Entry;
using SastWiki.Core.Contracts.InternalLink;
using SastWiki.Core.Contracts.User;
using SastWiki.Core.Models;
using SastWiki.Core.Models.Dto;
using SastWiki.Core.Services.Backend.Entry;
using SastWiki.Core.Services.InternalLink;
using SastWiki.WPF.Contracts;
using SastWiki.WPF.Views.Pages;

namespace SastWiki.WPF.ViewModels
{
    public partial class EditPageVM(
        IEntryProvider entryProvider,
        IUserLogin userLogin,
        INavigationService navigationService
    ) : ObservableObject, INavigationAware
    {
        Task<bool> INavigationAware.OnNavigatedFrom()
        {
            return Task.FromResult(true);
        }

        async Task<bool> INavigationAware.OnNavigatedTo<T>(T parameters)
        {
            if (parameters is int id)
            {
                await LoadEntry(id);
                return true;
            }
            return false;
        }

        [ObservableProperty]
        private int _id = 0;

        [ObservableProperty]
        private string _title = string.Empty;

        [ObservableProperty]
        private string _content = string.Empty;

        [ObservableProperty]
        private string _category = string.Empty;

        [ObservableProperty]
        private List<string> _tags = [];

        public ICommand SubmitCommand =>
            new RelayCommand(async () =>
            {
                await userLogin.LoginAsync("123456@xyz.com", "123456");
                var entry = new EntryDto
                {
                    Id = Id,
                    Title = Title,
                    Content = Content,
                    CategoryName = Category,
                    TagNames = Tags
                };
                try
                {
                    MessageBox.Show($"Update Entry :{entry}");
                    await entryProvider.UpdateEntryAsync(entry);
                    MessageBox.Show($"Entry id {Id} edited successfully", "Success");
                    await navigationService.NavigateTo(App.GetService<EntryViewPage>(), Id);
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

        public ICommand AddImageCommand =>
            new RelayCommand(() =>
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                if (openFileDialog.ShowDialog() == true)
                {
                    string imagePath = openFileDialog.FileName;

                    Content += $"!Image";
                }
            });

        private async Task LoadEntry(int id)
        {
            try
            {
                var currentEntry = await entryProvider.GetEntryByIdAsync(id);
                Id = currentEntry.Id ?? 0;
                Title = currentEntry.Title ?? "[UNTITLED]";
                Content = currentEntry.Content ?? "";
                Category = currentEntry.CategoryName ?? "";
                Tags = currentEntry.TagNames;
            }
            catch (ApiException e)
            {
                MessageBox.Show(e.Message, e.Content);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}
