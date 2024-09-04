using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlossomAvenue.Core.Products;

namespace BlossomAvenue.Service.CategoriesService
{
    public interface ICategoryManagement
    {
        public Task<IEnumerable<Category>> GetAllCategories();
        public Task<Category> CreateCategory(Category category);
        public Task<bool> UpdateCategory(Guid categoryId, UpdateCategoryDto updateCategoryDto);
        public Task<bool> DeleteCategory(Guid categoryId);

    }
}