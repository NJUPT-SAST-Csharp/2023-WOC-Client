using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SastWiki.Core.Models
{
    public class AppOptions
    {
        public string? AppName { get; set; }

        public string? AppVersion { get; set; }

        public string? AppDataPath { get; set; }

        public string? CacheBasePath { get; set; }

        public string? SettingsFilePath { get; set; }
    }
}
