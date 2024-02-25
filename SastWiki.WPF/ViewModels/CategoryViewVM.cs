using CommunityToolkit.Mvvm.ComponentModel;
using SastWiki.WPF.Contracts;

namespace SastWiki.WPF.ViewModels;

internal class CategoryViewVM : ObservableObject, INavigationAware
{
    Task<bool> INavigationAware.OnNavigatedFrom() => Task.FromResult(true);

    Task<bool> INavigationAware.OnNavigatedTo<T>(T parameters) => Task.FromResult(true);
}
