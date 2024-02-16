using SastWiki.Core.Contracts.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SastWiki.Core.Services.Infrastructure
{
    public class LocalStorage : ILocalStorage
    {
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
                    Console.WriteLine($"An error occurred while creating the file: {ex.Message}");
                    throw;
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
                    Console.WriteLine($"An error occurred while deleting the file: {ex.Message}");
                    throw;
                }
            });
        }

        public async Task<FileStream> GetFileStreamAsync(string absolutePath, string fileName)
        {
            return await Task.Run(() =>
            {
                try
                {
                    string filePath = System.IO.Path.Combine(absolutePath, fileName);

                    if (System.IO.File.Exists(filePath))
                    {
                        return new FileStream(filePath, FileMode.Open);
                    }
                    else
                    {
                        throw new FileNotFoundException($"File not found. {filePath}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(
                        $"An error occurred while getting the file stream: {ex.Message}"
                    );
                    throw;
                }
            });
        }
    }
}
