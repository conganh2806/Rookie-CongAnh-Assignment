using ECommerce.Application.Common.Utilities.Exceptions;
using ECommerce.Application.DTOs;
using ECommerce.Application.DTOs.Request;
using ECommerce.Domain.Entities;
using MockQueryable;
using Moq;

namespace ECommerce.Tests.Services.Products
{
    public partial class ProductServiceTests
    {
        [Fact]
        public async Task UpdateAsync_ShouldReturnUpdatedProductDTO_WhenValidRequest()
        {
            // Arrange
            var productId = "p-001";
            var originalProduct = new Product
            {
                Id = productId,
                Name = "Old Game",
                Categories = new List<Category>
                {
                    new Category { Id = "cat-001", Name = "Action" }
                }
            };

            var updateRequest = new ProductUpdateRequest
            {
                Name = "New Game",
                CategoryIds = new List<string> { "cat-002" }
            };

            var updatedCategories = new List<Category>
            {
                new Category { Id = "cat-002", Name = "Adventure" }
            };

            var updatedProduct = new Product
            {
                Id = productId,
                Name = "New Game",
                Categories = updatedCategories
            };

            var expectedDTO = new ProductDTO
            {
                Id = productId,
                Name = "New Game",
                CategoryNames = new List<string> { "Adventure" }
            };

            var mockProductQueryable = new List<Product> { originalProduct }
                .AsQueryable().BuildMock();

            _mockProductRepo.Setup(repo => repo.Entity)
                .Returns(mockProductQueryable);

            _mockCategoryRepo.Setup(repo => repo.Entity)
                .Returns(updatedCategories.AsQueryable().BuildMock());

            _mockMapper.Setup(m => m.Map(updateRequest, originalProduct))
                .Returns(updatedProduct);

            _mockMapper.Setup(m => m.Map<ProductDTO>(updatedProduct))
                .Returns(expectedDTO);

            // Act
            var result = await _productService.UpdateAsync(productId, updateRequest);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("New Game", result.Name);
            Assert.Contains("Adventure", result.CategoryNames);

            _mockProductRepo.Verify(r => r.Update(It.IsAny<Product>()), Times.Once);
            _mockProductRepo.Verify(r => r.UnitOfWork.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_ShouldThrowNotFoundException_WhenProductNotExists()
        {
            // Arrange
            var productId = "non-existent";
            var mockEmpty = new List<Product>().AsQueryable().BuildMock();

            _mockProductRepo.Setup(r => r.Entity)
                            .Returns(mockEmpty);

            var updateRequest = new ProductUpdateRequest
            {
                Name = "Doesn't Matter",
                CategoryIds = new List<string> { "cat-001" }
            };

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() =>
                _productService.UpdateAsync(productId, updateRequest));
        }

        [Fact]
        public async Task UpdateAsync_ShouldThrowBadRequestException_WhenCategoryIdsEmpty()
        {
            // Arrange
            var productId = "p-001";
            var product = new Product { Id = productId, Name = "Game" };

            var mockQueryable = new List<Product> { product }
                .AsQueryable().BuildMock();

            _mockProductRepo.Setup(r => r.Entity)
                            .Returns(mockQueryable);

            var request = new ProductUpdateRequest
            {
                Name = "New Name",
                CategoryIds = new List<string>() // empty!
            };

            // Act & Assert
            await Assert.ThrowsAsync<BadRequestException>(() =>
                _productService.UpdateAsync(productId, request));
        }

    }
}