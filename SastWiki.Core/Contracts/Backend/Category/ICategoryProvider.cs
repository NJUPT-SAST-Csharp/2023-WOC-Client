using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SastWiki.Core.Contracts.Backend.Category
{
    public interface ICategoryProvider
    {
        public Task<string> GetCategoryByIDAsync(int id);

        public Task<string> GetCategoryByFullNameAsync(string fullName);

        public Task<List<string>> GetAllCategoryList();

        public Task<Models.Result.CreateCategoryResult> CreateCategoryAsync(string categoryName);
    }
}
