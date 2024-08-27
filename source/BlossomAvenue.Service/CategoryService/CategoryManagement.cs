using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlossomAvenue.Core.Products;
using BlossomAvenue.Service.Repositories.Category;

namespace BlossomAvenue.Service.CategoryService
{
    public class CategoryManagement : ICategoryManagement
    {
        private ICategoryRepository _categoryRepository;

        public CategoryManagement(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<bool> CreateCategory(Category category)
        {
            return await _categoryRepository.CreateCategory(category);
        }

        public async Task<bool> DeleteCategory(Guid categoryId)
        {
            return await _categoryRepository.DeleteCategory(categoryId);
        }

        public async Task<IEnumerable<ICategory>> GetAllCategories()
        {
            return await _categoryRepository.GetAllCategories();
        }

        public async Task<bool> UpdateCategory(Guid categoryId, UpdateCategoryDto updateCategoryDto)
        {
            return await _categoryRepository.UpdateCategory(categoryId, updateCategoryDto);
        }
    }
}