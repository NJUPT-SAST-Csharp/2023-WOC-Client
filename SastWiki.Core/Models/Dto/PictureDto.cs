using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SastWiki.Core.Models.Dto
{
    public class PictureDto
    {
        public required int PictureId { get; set; }
        public required string PictureUrl { get; set; }
    }
}
