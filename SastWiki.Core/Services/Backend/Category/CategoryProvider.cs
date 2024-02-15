using SastWiki.Core.Contracts.Backend.Category;
using SastWiki.Core.Models.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SastWiki.Core.Services.Backend.Category
{
    public class CategoryProvider : ICategoryProvider
    {
        public CategoryProvider() { }

        public Task<CreateCategoryResult> CreateCategoryAsync(string categoryName)
        {
            throw new NotImplementedException();
        }

        public Task<CreateCategoryResult> CreateCategoryAsync(Models.Category category)
        {
            throw new NotImplementedException();
        }

        public Task<List<string>> GetAllCategoryList()
        {
            throw new NotImplementedException();
        }

        public Task<Models.Category> GetCategoryByFullNameAsync(string fullName)
        {
            throw new NotImplementedException();
        }

        public Task<Models.Category> GetCategoryByIDAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
