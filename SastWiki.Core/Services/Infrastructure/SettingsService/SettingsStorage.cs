using SastWiki.Core.Contracts.Infrastructure.SettingsService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SastWiki.Core.Services.Infrastructure.SettingsService
{
    public class SettingsStorage : ISettingsStorage
    {
        public SettingsStorage() { }

        public Task<string> GetSettingsJSON()
        {
            throw new NotImplementedException();
        }

        public Task SetSettingsJSON(string json)
        {
            throw new NotImplementedException();
        }
    }
}
