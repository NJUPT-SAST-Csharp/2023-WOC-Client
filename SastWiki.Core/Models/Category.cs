using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SastWiki.Core.Models
{
    public record Category
    {
        public required string Name { get; init; }

        public List<string>? Items { get; init; }
    }
}
