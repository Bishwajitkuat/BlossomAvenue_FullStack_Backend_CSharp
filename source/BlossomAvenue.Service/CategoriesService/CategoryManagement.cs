using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlossomAvenue.Core.Products;
using BlossomAvenue.Service.Repositories.Categories;


namespace BlossomAvenue.Service.CategoriesService
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

        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            return await _categoryRepository.GetAllCategories();
        }

        public async Task<bool> UpdateCategory(Guid categoryId, UpdateCategoryDto updateCategoryDto)
        {
            return await _categoryRepository.UpdateCategory(categoryId, updateCategoryDto);
        }
    }
}