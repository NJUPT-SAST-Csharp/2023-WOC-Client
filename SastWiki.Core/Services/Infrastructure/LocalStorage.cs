using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SastWiki.Core.Contracts.Infrastructure;

namespace SastWiki.Core.Services.Infrastructure
{
    public class LocalStorage : ILocalStorage
    {
        public async Task<bool> Contains(string absolutePath, string fileName)
        {
            return await Task.Run(() =>
            {
                try
                {
                    string filePath = System.IO.Path.Combine(absolutePath, fileName);
                    return System.IO.File.Exists(filePath);
                }
                catch (Exception ex)
                {
                    throw new Exception("An error occurred while checking if the file exists", ex);
                }
            });
        }

        public async Task CreateAsync(string absolutePath, string fileName)
        {
            await Task.Run(() =>
            {
                try
                {
                    string filePath = System.IO.Path.Combine(absolutePath, fileName);

                    System.IO.File.Create(filePath).Close();
                }
                catch (Exception ex)
                {
                    throw new Exception("An error occurred while creating the file", ex);
                }
            });
        }

        public async Task DeleteAsync(string absolutePath, string fileName)
        {
            await Task.Run(() =>
            {
                try
                {
                    string filePath = System.IO.Path.Combine(absolutePath, fileName);

                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }
                    else
                    {
                        throw new FileNotFoundException($"File not found. {filePath}");
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("An error occurred while deleting the file", ex);
                }
            });
        }

        public async Task<FileStream> GetFileStreamAsync(string absolutePath, string fileName) =>
            await Task.Run(() => GetFileStream(absolutePath, fileName));

        public FileStream GetFileStream(string absolutePath, string fileName)
        {
            try
            {
                string filePath = System.IO.Path.Combine(absolutePath, fileName);

                if (System.IO.File.Exists(filePath))
                {
                    return new FileStream(
                        filePath,
                        FileMode.Open,
                        FileAccess.ReadWrite,
                        FileShare.None
                    );
                }
                else
                {
                    throw new FileNotFoundException($"File not found. {filePath}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while getting the file stream", ex);
            }
        }
    }
}
