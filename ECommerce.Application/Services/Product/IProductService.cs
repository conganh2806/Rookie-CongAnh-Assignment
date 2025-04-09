using ECommerce.Application.DTOs;

namespace ECommerce.Application.Interfaces;

public interface IProductService
{
    Task<List<ProductDto>> GetFeaturedProductsAsync();
    Task<List<ProductDto>> GetByCategoryAsync(Guid categoryId);
    Task<ProductDto?> GetByIdAsync(Guid id);
}
