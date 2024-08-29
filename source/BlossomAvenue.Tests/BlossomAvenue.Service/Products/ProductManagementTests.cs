using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlossomAvenue.Core.Products;
using BlossomAvenue.Service.CustomExceptions;
using BlossomAvenue.Service.ProductsServices;
using BlossomAvenue.Service.Repositories.Products;
using Moq;
using Xunit;

namespace BlossomAvenue.Tests.BlossomAvenue.Service.Products
{
    public class ProductManagementTests
    {

        private IProductManagement _productMg;
        private Mock<IProductRepository> _mockProductRepo;

        public ProductManagementTests()
        {
            _mockProductRepo = new Mock<IProductRepository>();
            _productMg = new ProductManagement(_mockProductRepo.Object);
        }


        [Fact]
        public async Task CreateProduct_ValidData_ReturnProduct()
        {
            // Arrange
            var mockProduct = new Mock<Product>().Object;
            _mockProductRepo.Setup(x => x.CreateProduct(It.IsAny<Product>())).ReturnsAsync(mockProduct);

            // Act
            var result = await _productMg.CreateProduct(mockProduct);

            //Assert
            Assert.Equal(result, mockProduct);
        }

        [Fact]
        public async Task CreateProduct_InValidData_RiseException()
        {
            // Arrange
            var mockProduct = new Mock<Product>().Object;
            Product? nullProduct = null;
            var id = Guid.NewGuid();
            _mockProductRepo.Setup(x => x.CreateProduct(It.IsAny<Product>())).ReturnsAsync(nullProduct);

            // Act and Assert
            await Assert.ThrowsAsync<RecordNotCreatedException>(() =>
            _productMg.CreateProduct(mockProduct)
            );
        }


        [Fact]
        public async Task GetProductById_ValidData_ReturnGetProductByIdDto()
        {
            // Arrange
            var mockProduct = new Mock<Product>().Object;
            var id = Guid.NewGuid();
            _mockProductRepo.Setup(x => x.GetProductById(It.IsAny<Guid>())).ReturnsAsync(mockProduct);

            // Act
            var result = await _productMg.GetProductById(id);

            //Assert
            Assert.Equal(result, mockProduct);
        }


        [Fact]
        public async Task GetProductById_InValidData_RiseException()
        {
            // Arrange
            Product? mockProduct = null;
            var id = Guid.NewGuid();
            _mockProductRepo.Setup(x => x.GetProductById(It.IsAny<Guid>())).ReturnsAsync(mockProduct);

            // Act and Assert
            await Assert.ThrowsAsync<RecordNotFoundException>(() =>
            _productMg.GetProductById(id)
            );
        }

        [Fact]
        public async Task UpdateProduct_ValidData_ReturnTrue()
        {
            // Arrange
            var mockProduct = new Mock<Product>().Object;
            _mockProductRepo.Setup(x => x.UpdateProduct(It.IsAny<Guid>(), It.IsAny<Product>())).ReturnsAsync(true);

            // Act
            var result = await _productMg.UpdateProduct(Guid.NewGuid(), mockProduct);

            //Assert
            Assert.True(result);
        }


        [Fact]
        public async Task UpdateProduct_InValidData_RiseException()
        {
            // Arrange
            var mockProduct = new Mock<Product>().Object;
            _mockProductRepo.Setup(x => x.UpdateProduct(It.IsAny<Guid>(), It.IsAny<Product>())).ReturnsAsync(false);

            // Act and Assert
            await Assert.ThrowsAsync<RecordNotFoundException>(() =>
            _productMg.UpdateProduct(Guid.NewGuid(), mockProduct)
            );
        }


        [Fact]
        public async Task DeleteProductById_ValidData_ReturnTrue()
        {
            // Arrange
            var id = Guid.NewGuid();
            _mockProductRepo.Setup(x => x.DeleteProductById(It.IsAny<Guid>())).ReturnsAsync(true);

            // Act
            var result = await _productMg.DeleteProductById(Guid.NewGuid());

            //Assert
            Assert.True(result);
        }


        [Fact]
        public async Task DeleteProductById_InValidData_RiseException()
        {
            // Arrange
            var id = Guid.NewGuid();
            _mockProductRepo.Setup(x => x.DeleteProductById(It.IsAny<Guid>())).ReturnsAsync(false);

            // Act and Assert
            await Assert.ThrowsAsync<RecordNotFoundException>(() =>
            _productMg.GetProductById(id)
            );
        }


        [Fact]
        public async Task GetAllProducts_ValidData_ReturnGetAllProductReadDto()
        {
            // Arrange
            var readDtos = new List<Product>{
                new Mock<Product>().Object,
                new Mock<Product>().Object,
                new Mock<Product>().Object,
            };
            var mockPQDto = new Mock<ProductQueryDto>().Object;
            _mockProductRepo.Setup(x => x.GetAllProducts(It.IsAny<ProductQueryDto>())).ReturnsAsync(readDtos);

            // Act
            var result = await _productMg.GetAllProducts(mockPQDto);

            //Assert
            Assert.Equal(readDtos, result);
        }









    }
}