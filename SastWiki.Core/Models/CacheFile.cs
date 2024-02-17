using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SastWiki.Core.Models
{
    public class CacheFile
    {
        public string FileName { get; set; }
        public DateTime UpdatedTime { get; set; }
        public TimeSpan ExpireTime { get; set; }
    }
}
