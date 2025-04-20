using ECommerce.Application.Common.Utilities.Exceptions;
using ECommerce.Application.DTOs;
using ECommerce.Domain.Entities;
using MockQueryable;
using Moq;

namespace ECommerce.Tests.Services.Products
{
    public partial class ProductServiceTests
    {
        [Fact]
        public async Task GetAllAsync_ShouldReturnProductList()
        {
            // Arrange
            var products = new List<Product>
            {
                new Product { Id = "1", Name = "Game A", Price = 9.99m },
                new Product { Id = "2", Name = "Game B", Price = 19.99m },
            };

            // Mock IQueryable<Product>
            var mockQueryable = products.AsQueryable().BuildMock();

            _mockProductRepo.Setup(r => r.Entity)
                            .Returns(mockQueryable);

            // Act
            var result = await _productService.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Contains(result, p => p.Name == "Game A");
            Assert.Contains(result, p => p.Name == "Game B");
        }

        [Fact]
        public async Task GetByCategoryAsync_ShouldReturnProductsInCategory()
        {
            // Arrange
            var categoryId = "cat-001";

            var products = new List<Product>
            {
                new Product 
                { 
                    Id = "1", 
                    Name = "Game A", 
                    Categories = new List<Category>
                    {
                        new Category { Id = categoryId, Name = "Action" }
                    } 
                },
                new Product 
                { 
                    Id = "2", 
                    Name = "Game B", 
                    Categories = new List<Category>
                    {
                        new Category { Id = "cat-002", Name = "RPG" }
                    } 
                }
            };

            var mockQueryable = products.AsQueryable().BuildMock();

            _mockProductRepo.Setup(r => r.Entity)
                            .Returns(mockQueryable);

            // Act
            var result = await _productService.GetByCategoryAsync(categoryId);

            // Assert
            Assert.NotNull(result);
            Assert.Single(result); // Chỉ có 1 product thuộc categoryId
            Assert.Equal("Game A", result[0].Name);
        }

        [Fact]
        public async Task GetDetails_ShouldReturnProductDetail_WhenProductExists()
        {
            // Arrange
            var productId = "p-001";
            var product = new Product
            {
                Id = productId,
                Name = "Game X",
                Categories = new List<Category>
                {
                    new Category { Id = "cat-001", Name = "Action" },
                    new Category { Id = "cat-002", Name = "Adventure" }
                }
            };

            var expectedResponse = new ProductDetailResponse
            {
                Id = productId,
                Name = "Game X",
                CategoryNames = new List<string> { "Action", "Adventure" }
            };

            var mockQueryable = new List<Product> { product }.AsQueryable().BuildMock();

            _mockProductRepo.Setup(r => r.Entity)
                .Returns(mockQueryable);

            _mockMapper.Setup(m => m.Map<ProductDetailResponse>(product))
                .Returns(new ProductDetailResponse
                {
                    Id = productId,
                    Name = "Game X"
                });

            // Act
            var result = await _productService.GetDetails(productId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(productId, result?.Id);
            Assert.Equal("Game X", result?.Name);
            Assert.Equal(2, result?.CategoryNames.Count);
            Assert.Contains("Action", result?.CategoryNames);
            Assert.Contains("Adventure", result?.CategoryNames);
        }

        [Fact]
        public async Task GetDetails_ShouldThrowNotFoundException_WhenProductNotFound()
        {
            // Arrange
            var productId = "non-existent";

            var emptyList = new List<Product>().AsQueryable().BuildMock();

            _mockProductRepo.Setup(r => r.Entity).Returns(emptyList);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<NotFoundException>(() =>
                                         _productService.GetDetails(productId));

            Assert.Equal($"Product with id {productId} not found.", exception.Message);
        }

         [Fact]
        public async Task GetFeaturedAsync_ShouldReturnFeaturedProducts_WhenProductsExist()
        {
            // Arrange
            var featuredProducts = new List<Product>
            {
                new Product { Id = "1", Name = "Featured Game A", Price = 9.99m, IsFeatured = true },
                new Product { Id = "2", Name = "Featured Game B", Price = 19.99m, IsFeatured = true }
            };

            var featuredProductResponses = new List<ProductFeatureResponse>
            {
                new ProductFeatureResponse { Id = "1", Name = "Featured Game A", Price = 9.99m },
                new ProductFeatureResponse { Id = "2", Name = "Featured Game B", Price = 19.99m }
            };

            _mockProductRepo.Setup(r => r.Entity)
                .Returns(featuredProducts.AsQueryable().BuildMock());

            _mockMapper.Setup(m => m.Map<List<ProductFeatureResponse>>(It.IsAny<List<Product>>()))
                .Returns(featuredProductResponses);

            // Act
            var result = await _productService.GetFeaturedAsync(2);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal("Featured Game A", result[0].Name);
            Assert.Equal("Featured Game B", result[1].Name);

            // Verify sorting by price
            Assert.True(result[0].Price < result[1].Price);
        }

        [Fact]
        public async Task GetFeaturedAsync_ShouldReturnEmptyList_WhenNoFeaturedProductsExist()
        {
            // Arrange
            var nonFeaturedProducts = new List<Product>
            {
                new Product { Id = "1", Name = "Non-Featured Game A", Price = 9.99m, IsFeatured = false },
                new Product { Id = "2", Name = "Non-Featured Game B", Price = 19.99m, IsFeatured = false }
            };

            _mockProductRepo.Setup(r => r.Entity)
                .Returns(nonFeaturedProducts.AsQueryable().BuildMock());

            // Act
            var result = await _productService.GetFeaturedAsync(2);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

         [Fact]
        public async Task SearchProductsAsync_ShouldReturnEmptyList_WhenKeywordIsNullOrWhiteSpace()
        {
            // Arrange
            string keyword = " ";

            // Act
            var result = await _productService.SearchProductsAsync(keyword);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task SearchProductsAsync_ShouldReturnProducts_WhenKeywordMatchesNameOrDescription()
        {
            // Arrange
            var keyword = "Game";

            var products = new List<Product>
            {
                new Product { Id = "1", Name = "Game A", Description = "Action Game", Price = 9.99m },
                new Product { Id = "2", Name = "Game B", Description = "RPG Game", Price = 19.99m }
            };

            var productDTOs = new List<ProductDTO>
            {
                new ProductDTO { Id = "1", Name = "Game A", Description = "Action Game", Price = 9.99m },
                new ProductDTO { Id = "2", Name = "Game B", Description = "RPG Game", Price = 19.99m }
            };

            // Mock the repository and mapping
            _mockProductRepo.Setup(r => r.Entity)
                .Returns(products.AsQueryable().BuildMock());

            _mockMapper.Setup(m => m.Map<List<ProductDTO>>(It.IsAny<List<Product>>()))
                .Returns(productDTOs);

            // Act
            var result = await _productService.SearchProductsAsync(keyword);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Contains(result, p => p.Name.Contains(keyword) || p.Description.Contains(keyword));
        }

        [Fact]
        public async Task SearchProductsAsync_ShouldReturnEmptyList_WhenNoProductsMatchKeyword()
        {
            // Arrange
            var keyword = "NonExistentGame";

            var products = new List<Product>
            {
                new Product { Id = "1", Name = "Game A", Description = "Action Game", Price = 9.99m },
                new Product { Id = "2", Name = "Game B", Description = "RPG Game", Price = 19.99m }
            };

            // Mock the repository
            _mockProductRepo.Setup(r => r.Entity)
                .Returns(products.AsQueryable().BuildMock());

            // Act
            var result = await _productService.SearchProductsAsync(keyword);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result); // No products should match the keyword
        }
    }
}