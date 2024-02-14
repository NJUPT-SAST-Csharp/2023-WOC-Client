using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SastWiki.Core.Models
{
    public record Entry
    {
        public required string Title { get; init; }
        public required string Content { get; init; }

        // public DateTime CreatedAt { get; init; }
        // public DateTime UpdatedAt { get; init; }
        public Category Category { get; init; } = new Category() { Name = String.Empty };
        public IEnumerable<Tag> Tags { get; init; } = new List<Tag>();
    }
}
