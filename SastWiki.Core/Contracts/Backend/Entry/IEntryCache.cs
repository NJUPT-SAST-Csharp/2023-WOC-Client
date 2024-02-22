using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SastWiki.Core.Contracts.Infrastructure.CacheService;
using SastWiki.Core.Models.Dto;

namespace SastWiki.Core.Contracts.Backend.Entry
{
    public interface IEntryCache : ICache<EntryDto> // 并不一定是string
    {
        public List<EntryDto>? EntryMetadataList { get; set; }
    }
}
