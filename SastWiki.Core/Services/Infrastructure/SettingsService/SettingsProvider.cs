using SastWiki.Core.Contracts.Infrastructure.SettingsService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SastWiki.Core.Services.Infrastructure.SettingsService
{
    public class SettingsProvider : ISettingsProvider
    {
        public SettingsProvider() { }

        public Task<T> GetItem<T>(string label)
        {
            throw new NotImplementedException();
        }

        public Task SetItem<T>(string label, T item)
        {
            throw new NotImplementedException();
        }
    }
}
