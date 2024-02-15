using SastWiki.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SastWiki.Core.Contracts.Backend.Tag
{
    public interface ITagProvider
    {
        /// <summary>
        /// （从后端获取到的）所有的Tags
        /// </summary>
        public Task<IEnumerable<string>> GetAllTagsList();

        /// <summary>
        /// 获取某个Tag下全部的词条的ID
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        public Task<IEnumerable<int>> GetEntryIdListByTag(string tag);
    }
}
