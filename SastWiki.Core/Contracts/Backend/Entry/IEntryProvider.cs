using SastWiki.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SastWiki.Core.Contracts.Backend.Entry
{
    public interface IEntryProvider
    {
        /// <summary>
        /// 获取指定ID的词条
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<Models.Entry> GetEntryByIdAsync(int id);

        /// <summary>
        /// 请求增加一个词条并返回新词条的id
        /// </summary>
        /// <param name="entry"></param>
        /// <returns>Returns the id if complete, -1 if not successful</returns>
        public Task<int> AddEntryAsync(Models.Entry entry);

        /// <summary>
        /// 请求修改一个词条，与AddEntryAsync的区别在于会使用传入Entry的id
        /// </summary>
        /// <param name="entry"></param>
        /// <returns>Return true if successful</returns>
        public Task<bool> UpdateEntryAsync(Models.Entry entry);

        /// <summary>
        /// 返回是否存在某个词条
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<bool> IsEntryExistsAsync(int id);
    }
}
