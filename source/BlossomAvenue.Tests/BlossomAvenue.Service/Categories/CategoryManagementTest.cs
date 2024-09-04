using System;
using BlossomAvenue.Core.Products;
using BlossomAvenue.Service.CategoriesService;
using BlossomAvenue.Service.CustomExceptions;
using BlossomAvenue.Service.Repositories.Categories;
using Moq;
using Xunit;

namespace BlossomAvenue.Tests.BlossomAvenue.Service.Categories
{
    public class CategoryManagementTest
    {
        private ICategoryManagement _categoryManagement;
        private Mock<ICategoryRepository> _mockCategoryRepository;

        public CategoryManagementTest()
        {
            _mockCategoryRepository = new Mock<ICategoryRepository>();
            _categoryManagement = new CategoryManagement(_mockCategoryRepository.Object);
        }

        [Fact]
        public async Task CreateCategory_ValidData_ReturnCategory()
        {
            // Arrange
            var mockCategory = new Mock<Category>().Object;

            _mockCategoryRepository.Setup(x => x.CreateCategory(It.IsAny<Category>())).ReturnsAsync(mockCategory);

            // Act
            var result = await _categoryManagement.CreateCategory(mockCategory);
            // Assert
            Assert.Equal(result, mockCategory);
        }


        [Fact]
        public async Task CreateCategory_InvalidData_RiseException()
        {
            // Arrange
            var mockCategory = new Mock<Category>().Object;
            Category? nullMockcategory = null;
            _mockCategoryRepository.Setup(x => x.CreateCategory(It.IsAny<Category>())).ReturnsAsync(nullMockcategory);

            // Act & Assert
            await Assert.ThrowsAsync<RecordNotCreatedException>(
            () => _categoryManagement.CreateCategory(mockCategory)
            );
        }


        [Fact]
        public async Task DeleteCategory_ValidData_ReturnTrue()
        {
            // Arrange
            var id = Guid.NewGuid();
            _mockCategoryRepository.Setup(x => x.DeleteCategory(It.IsAny<Guid>())).ReturnsAsync(true);

            // Act
            var result = await _categoryManagement.DeleteCategory(id);
            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task DeleteCategory_InvalidData_RiseException()
        {
            // Arrange
            var id = Guid.NewGuid();
            _mockCategoryRepository.Setup(x => x.DeleteCategory(It.IsAny<Guid>())).ReturnsAsync(false);

            // Act & Assert
            await Assert.ThrowsAsync<RecordNotFoundException>(
            () => _categoryManagement.DeleteCategory(id)
            );
        }

        [Fact]
        public async Task UpdateCategory_ValidData_ReturnTrue()
        {
            // Arrange
            var mockUpdateCategoryDto = new Mock<UpdateCategoryDto>().Object;
            var id = Guid.NewGuid();
            _mockCategoryRepository.Setup(x => x.UpdateCategory(It.IsAny<Guid>(), It.IsAny<UpdateCategoryDto>())).ReturnsAsync(true);

            // Act
            var result = await _categoryManagement.UpdateCategory(id, mockUpdateCategoryDto);
            // Assert
            Assert.True(result);

        }


        [Fact]
        public async Task UpdateCategory_InvalidData_RiseException()
        {
            // Arrange
            var mockUpdateCategoryDto = new Mock<UpdateCategoryDto>().Object;
            var id = Guid.NewGuid();
            _mockCategoryRepository.Setup(x => x.UpdateCategory(It.IsAny<Guid>(), It.IsAny<UpdateCategoryDto>())).ReturnsAsync(false);

            // Act & Assert
            await Assert.ThrowsAsync<RecordNotUpdatedException>(
            () => _categoryManagement.UpdateCategory(id, mockUpdateCategoryDto)
            );
        }


        [Fact]
        public async Task GetAllCategories_ValidData_ReturnTrue()
        {
            // Arrange
            IEnumerable<Category> categories = new List<Category>{
                new Mock<Category>().Object,
                new Mock<Category>().Object,
                new Mock<Category>().Object
            };
            var id = Guid.NewGuid();
            _mockCategoryRepository.Setup(x => x.GetAllCategories()).ReturnsAsync(categories);

            // Act
            var result = await _categoryManagement.GetAllCategories();
            // Assert
            Assert.Equal(categories, result);
            Assert.NotNull(result);
            Assert.IsType<List<Category>>(result);

        }







    }
}