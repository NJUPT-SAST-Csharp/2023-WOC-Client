using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SastWiki.Core.Contracts.Infrastructure
{
    /// <summary>
    /// 对本地文件进行操作的接口
    /// </summary>
    public interface ILocalStorage
    {
        /// <summary>
        /// 获取指定文件的文件流，没有则新建一个文件
        /// </summary>
        /// <param name="absolutePath"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public Task<FileStream> GetFileStreamAsync(string absolutePath, string fileName); // TODO: 检查一下有没有地方忘了关闭文件流

        /// <summary>
        /// 删除指定文件，删不掉就报错
        /// </summary>
        /// <param name="absolutePath"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public Task DeleteAsync(string absolutePath, string fileName);

        /// <summary>
        /// 创建指定文件
        /// </summary>
        /// <param name="absolutePath"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public Task CreateAsync(string absolutePath, string fileName);

        public Task<bool> Contains(string absolutePath, string fileName);

        public FileStream GetFileStream(string absolutePath, string fileName);
    }
}
