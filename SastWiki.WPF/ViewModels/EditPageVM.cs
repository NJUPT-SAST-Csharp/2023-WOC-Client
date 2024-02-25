using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Options;
using Microsoft.Win32;
using Refit;
using SastWiki.Core.Contracts.Backend.Entry;
using SastWiki.Core.Contracts.Backend.Image;
using SastWiki.Core.Contracts.InternalLink;
using SastWiki.Core.Contracts.User;
using SastWiki.Core.Models;
using SastWiki.Core.Models.Dto;
using SastWiki.Core.Services.Backend.Entry;
using SastWiki.Core.Services.InternalLink;
using SastWiki.WPF.Contracts;
using SastWiki.WPF.Views.Pages;
using static Unity.Storage.RegistrationSet;

namespace SastWiki.WPF.ViewModels
{
    public partial class EditPageVM(
        IEntryProvider entryProvider,
        IImageProvider imageProvider,
        INavigationService navigationService,
        IOptions<AppOptions> options
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
        private bool isUploading = false;

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
            new RelayCommand(
                async () =>
                {
                    IsUploading = true;
                    UpdateButtons();
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
                        var newentry =
                            (Id == 0)
                                ? await entryProvider.AddEntryAsync(entry)
                                : await entryProvider.UpdateEntryAsync(entry);
                        await navigationService.NavigateTo(
                            App.GetService<EntryViewPage>(),
                            newentry.Id
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
                    IsUploading = false;
                    UpdateButtons();
                },
                () => !IsUploading
            );

        public ICommand AddImageCommand =>
            new RelayCommand(
                async () =>
                {
                    OpenFileDialog openFileDialog = new OpenFileDialog();
                    if (openFileDialog.ShowDialog() == true)
                    {
                        string imagePath = openFileDialog.FileName;
                        try
                        {
                            var imgBytes = await File.ReadAllBytesAsync(imagePath);
                            var uploadedImg = await imageProvider.UploadImageAsync(imgBytes);
                            MessageBox.Show($"Upload success! Image Id is {uploadedImg.PictureId}");
                            Content +=
                                $"\n\n![](http://{options.Value.HostName}/api/Picture/GetPictureById?id={uploadedImg.PictureId})";
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
                },
                () => !IsUploading
            );

        public ICommand DeleteCommand =>
            new RelayCommand(
                async () =>
                {
                    if (Id == 0)
                    {
                        return;
                    }
                    try
                    {
                        await entryProvider.DeleteEntryAsync(Id);
                    }
                    catch (ApiException e)
                    {
                        MessageBox.Show(e.Message, e.Content);
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message);
                    }
                },
                () => !IsUploading && Id != 0
            );

        private async Task LoadEntry(int id)
        {
            IsUploading = true;
            UpdateButtons();
            if (id == 0)
            {
                Id = 0;
                Title = "";
                Content = "";
                Category = "";
                Tags = [];
                IsUploading = false;
                UpdateButtons();
                return;
            }
            try
            {
                var currentEntry = await entryProvider.GetEntryByIdAsync(id);
                Id = currentEntry.Id ?? 0;
                Title = currentEntry.Title ?? "[UNTITLED]";
                Content = currentEntry.Content ?? "";
                Category = currentEntry.CategoryName ?? "";
                Tags = currentEntry.TagNames;
                IsUploading = false;
                UpdateButtons();
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

        void UpdateButtons()
        {
            OnPropertyChanged(nameof(SubmitCommand));
            OnPropertyChanged(nameof(AddImageCommand));
            OnPropertyChanged(nameof(DeleteCommand));
        }
    }
}
