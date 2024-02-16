using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SastWiki.Core.Models.Api
{
    public record EntryDto
    {
        public required string Title { get; set; }
        public required string Content { get; set; }
        public required string CategoryName { get; set; }
        public required List<string> TagNames { get; set; } = [];
    }
}
