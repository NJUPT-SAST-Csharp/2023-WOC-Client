using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SastWiki.Core.Models.Dto
{
    public record EntryDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public string? CategoryName { get; set; }
        public List<string> TagNames { get; set; } = [];
    }
}
