using ECommerce.Domain.Entities;

namespace ECommerce.Application.Domain.Interfaces
{
    public interface IProductRepository
    {
        Task<List<Product>> GetFeaturedProductsAsync();
        Task<Product?> GetByIdAsync(Guid id);
        Task<List<Product>> GetByCategoryAsync(Guid categoryId);
    }
}