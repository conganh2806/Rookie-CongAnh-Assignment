using ECommerce.Domain.Entities;
using ECommerce.Domain.Interfaces;
using MockQueryable;
using Moq;

namespace ECommerce.Tests.Services.Products
{
    public partial class ProductServiceTests
    {
        
        [Fact]
        public async Task DeleteAsync_ShouldDeleteProduct_WhenProductExists()
        {
            // Arrange
            var productId = "p-001";
            var existingProduct = new Product { Id = productId, Name = "Test Game" };

            var mockQueryable = new List<Product> { existingProduct }.AsQueryable().BuildMock();

            _mockProductRepo.Setup(r => r.Entity).Returns(mockQueryable);

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockProductRepo.Setup(r => r.UnitOfWork).Returns(mockUnitOfWork.Object);

            // Act
            await _productService.DeleteAsync(productId);

            // Assert
            _mockProductRepo.Verify(r => r.Delete(It.Is<Product>(p => p.Id == productId)), Times.Once);
            mockUnitOfWork.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}