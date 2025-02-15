using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlossomAvenue.Core.Products;
using BlossomAvenue.Infrastructure.Database;
using BlossomAvenue.Service.CategoriesService;
using BlossomAvenue.Service.CustomExceptions;
using BlossomAvenue.Service.Repositories.Categories;
using Microsoft.EntityFrameworkCore;

namespace BlossomAvenue.Infrastructure.Repositories.Categories
{
    public class CategoryRepository : ICategoryRepository
    {
        private BlossomAvenueDbContext _context;

        public CategoryRepository(BlossomAvenueDbContext context)
        {
            _context = context;
        }
        public async Task<Category?> CreateCategory(Category category)
        {
            var newCategory = (await _context.Categories.AddAsync(category)).Entity;
            _context.SaveChanges();
            return newCategory;
        }

        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category> UpdateCategory(Guid categoryId, UpdateCategoryDto updateCategoryDto)
        {
            Category category = await _context.Categories.Where(c => c.CategoryId == categoryId).FirstOrDefaultAsync<Category>();
            if (category is null) throw new RecordNotFoundException("category");
            category.CategoryName = updateCategoryDto.CategoryName;
            category.ParentId = updateCategoryDto.ParentId;
            if (await _context.SaveChangesAsync() > 0) return category;
            else throw new RecordNotUpdatedException("category");
        }

        public async Task<bool> DeleteCategory(Guid categoryId)
        {
            Category category = await _context.Categories.Where(c => c.CategoryId == categoryId).FirstOrDefaultAsync<Category>();
            if (category is null) return false;
            _context.Categories.Remove(category);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}