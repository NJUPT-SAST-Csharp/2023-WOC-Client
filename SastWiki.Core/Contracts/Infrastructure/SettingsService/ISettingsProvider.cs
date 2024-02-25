namespace SastWiki.Core.Contracts.Infrastructure.SettingsService;

public interface ISettingsProvider
{
    public Task<T?> GetItem<T>(string label);
    public Task SetItem<T>(string label, T item);
}
