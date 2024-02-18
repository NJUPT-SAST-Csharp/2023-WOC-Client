using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SastWiki.Core.Models.Dto;

namespace SastWiki.Core.Models.Result
{
    public class UpdateEntryResult
    {
        public required EntryDto UpdatedEntry { get; set; }
        public int StatusCode { get; set; }
    }
}
