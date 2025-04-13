using ECommerce.Application.DTOs;

namespace ECommerce.Application.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductDTO>> GetAllAsync();
        Task<List<ProductDTO>> GetByCategoryAsync(string categoryId);
        Task<ProductDTO?> GetByIdAsync(string id);
        Task<List<ProductDTO>> GetBySlugAsync(string slug);
    }
}

