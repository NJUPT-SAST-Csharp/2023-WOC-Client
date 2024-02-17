using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SastWiki.Core.Contracts.Infrastructure.SettingsService
{
    public interface ISettingsProvider
    {
        public Task<T?> GetItem<T>(string label);
        public Task SetItem<T>(string label, T item);
    }
}
