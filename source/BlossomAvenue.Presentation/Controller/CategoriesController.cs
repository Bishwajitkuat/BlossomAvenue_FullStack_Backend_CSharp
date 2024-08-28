using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlossomAvenue.Core.Products;
using BlossomAvenue.Service.CategoriesService;
using Microsoft.AspNetCore.Mvc;

namespace BlossomAvenue.Presentation.Controller
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CategoriesController : ControllerBase
    {

        private ICategoryManagement _categoryManagement;

        public CategoriesController(ICategoryManagement categoryManagement)
        {
            _categoryManagement = categoryManagement;
        }

        [HttpGet("")]
        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            return await _categoryManagement.GetAllCategories();

        }
        // ADMIN USER
        [HttpPost("")]
        public async Task<bool> CreateCategory(CreateCategoryDto createCategoryDto)
        {
            var category = createCategoryDto.ConvertToCategory();
            return await _categoryManagement.CreateCategory(category);
        }

        // ADMIN USER
        [HttpPatch("{id}")]
        public async Task<bool> UpdateCategory([FromRoute] Guid id, [FromBody] UpdateCategoryDto updateCategoryDto)
        {
            Console.WriteLine($"id");

            return await _categoryManagement.UpdateCategory(id, updateCategoryDto);

        }

        // ADMIN USER
        [HttpDelete("{id}")]
        public async Task<bool> DeleteCategory([FromRoute] Guid id)
        {

            return await _categoryManagement.DeleteCategory(id);

        }
    }
}