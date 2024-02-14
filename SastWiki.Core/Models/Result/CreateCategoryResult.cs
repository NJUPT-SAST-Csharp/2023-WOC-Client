using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SastWiki.Core.Models.Result
{
    public record CreateCategoryResult
    {
        public required string ID { get; init; }
        public required string Name { get; init; }
        public required bool IsSuccess { get; init; }
        public string? Message { get; init; }
    }
}
