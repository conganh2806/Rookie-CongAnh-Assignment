using ECommerce.Application.DTOs;

namespace ECommerce.Application.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDTO>> GetAllAsync();
        Task<IEnumerable<ProductDTO>> GetByCategoryAsync(string categoryId);
        Task<ProductDTO?> GetByIdAsync(string id);
        Task<IEnumerable<ProductDTO>> GetBySlugAsync(string slug);
    }
}

