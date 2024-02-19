using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SastWiki.Core.Contracts.Backend.Category;

namespace SastWiki.Core.Services.Backend.Category
{
    public class CategoryProvider : ICategoryProvider
    {
        public CategoryProvider() { }

        public Task CreateCategoryAsync(string categoryName)
        {
            throw new NotImplementedException();
        }

        public Task<List<string>> GetAllCategoryList()
        {
            throw new NotImplementedException();
        }

        public Task<string> GetCategoryByFullNameAsync(string fullName)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetCategoryByIDAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
