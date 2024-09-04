using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlossomAvenue.Core.Products;
using BlossomAvenue.Service.CustomExceptions;
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

        public async Task<Category> CreateCategory(Category category)
        {
            var newCategory = await _categoryRepository.CreateCategory(category);
            if (newCategory == null) throw new RecordNotCreatedException("category");
            return newCategory;
        }

        public async Task<bool> DeleteCategory(Guid categoryId)
        {
            var result = await _categoryRepository.DeleteCategory(categoryId);
            if (result == false) throw new RecordNotFoundException("category");
            return result;
        }

        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            return await _categoryRepository.GetAllCategories();
        }

        public async Task<bool> UpdateCategory(Guid categoryId, UpdateCategoryDto updateCategoryDto)
        {
            var result = await _categoryRepository.UpdateCategory(categoryId, updateCategoryDto);
            if (result is false) throw new RecordNotUpdatedException("category");
            return result;
        }
    }
}