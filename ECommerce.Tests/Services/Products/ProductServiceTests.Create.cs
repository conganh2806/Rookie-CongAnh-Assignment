using ECommerce.Application.DTOs;
using ECommerce.Domain.Entities;
using MockQueryable;
using Moq;

namespace ECommerce.Tests.Services.Products
{
    public partial class ProductServiceTests
    {
        [Fact]
        public async Task CreateAsync_ShouldReturnProductDTO_WhenProductIsCreated()
        {
            // Arrange
            var category1 = new Category { Id = "cat-001", Name = "Action" };
            var category2 = new Category { Id = "cat-002", Name = "Adventure" };

            var productCreateRequest = new ProductCreateRequest
            {
                Name = "Game X",
                CategoryIds = new List<string> { "cat-001", "cat-002" }
            };

            var categoriesFromRepo = new List<Category> { category1, category2 };
            var product = new Product
            {
                Id = "p-001",
                Name = "Game X",
                Categories = categoriesFromRepo
            };

            var expectedProductDTO = new ProductDTO
            {
                Id = "p-001",
                Name = "Game X",
                CategoryNames = new List<string> { "Action", "Adventure" }
            };

            _mockCategoryRepo.Setup(repo => repo.Entity)
                .Returns(categoriesFromRepo.AsQueryable().BuildMock()); // Mock IQueryable

            _mockMapper.Setup(mapper => mapper.Map<Product>(It.IsAny<ProductCreateRequest>()))
                .Returns(product);

            _mockMapper.Setup(mapper => mapper.Map<ProductDTO>(It.IsAny<Product>()))
                .Returns(expectedProductDTO);
                
            // Act
            var result = await _productService.CreateAsync(productCreateRequest);

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result?.Id);
            Assert.Equal("Game X", result?.Name);
            Assert.Contains("Action", result?.CategoryNames);
            Assert.Contains("Adventure", result?.CategoryNames);

            _mockProductRepo.Verify(repo => repo.Add(It.IsAny<Product>()), Times.Once);
            _mockProductRepo.Verify(repo => 
                repo.UnitOfWork.SaveChangesAsync(CancellationToken.None), 
                Times.Once);
        }
    }
}