using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlossomAvenue.Core.Products;
using BlossomAvenue.Service.CategoryService;


namespace BlossomAvenue.Service.Repositories.Category
{
    public interface ICategoryRepository
    {
        public Task<IEnumerable<ICategory>> GetAllCategories();
        public Task<bool> CreateCategory(ICategory category);
        public Task<bool> UpdateCategory(Guid categoryId, UpdateCategoryDto updateCategoryDto);
        public Task<bool> DeleteCategory(Guid categoryId);

    }
}