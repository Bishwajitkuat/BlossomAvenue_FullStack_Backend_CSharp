using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlossomAvenue.Core.Products;
using BlossomAvenue.Service.CategoriesService;
using Microsoft.AspNetCore.Authorization;
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

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _categoryManagement.GetAllCategories();
            return Ok(categories);

        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryDto createCategoryDto)
        {
            var category = createCategoryDto.ConvertToCategory();
            var newCategory = await _categoryManagement.CreateCategory(category);
            return Created(nameof(CreateCategory), newCategory);
        }

        [Authorize(Roles = "Admin")]
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateCategory([FromRoute] Guid id, [FromBody] UpdateCategoryDto updateCategoryDto)
        {

            await _categoryManagement.UpdateCategory(id, updateCategoryDto);
            return NoContent();

        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory([FromRoute] Guid id)
        {

            await _categoryManagement.DeleteCategory(id);
            return NoContent();

        }
    }
}