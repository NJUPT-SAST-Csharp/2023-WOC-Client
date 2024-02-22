using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SastWiki.Core.Models;
using SastWiki.Core.Models.Dto;
using SastWiki.Core.Services.Backend.Entry;

namespace SastWiki.Core.Contracts.Backend.Tag
{
    public interface ITagProvider
    {
        /// <summary>
        /// （从后端获取到的）所有的Tags
        /// </summary>
        public Task<List<string>> GetAllTagsList();

        /// <summary>
        /// 获取某组Tag下全部的词条的ID
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        public Task<List<EntryDto>> GetEntryByTags(List<string> tags);
    }
}
