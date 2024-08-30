using System;
using System.Collections.Generic;
using System.Linq;
using BlossomAvenue.Core.Products;
using BlossomAvenue.Service.CategoriesService;


namespace BlossomAvenue.Service.Repositories.Categories
{
    public interface ICategoryRepository
    {
        public Task<IEnumerable<Category>> GetAllCategories();
        public Task<Category?> CreateCategory(Category category);
        public Task<bool> UpdateCategory(Guid categoryId, UpdateCategoryDto updateCategoryDto);
        public Task<bool> DeleteCategory(Guid categoryId);

    }
}