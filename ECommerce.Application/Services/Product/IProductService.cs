using ECommerce.Application.DTOs;
using ECommerce.Application.DTOs.Request;
using ECommerce.Application.Services;

namespace ECommerce.Application.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductDTO>> GetAllAsync();
        Task<List<ProductDTO>> GetByCategoryAsync(string categoryId);
        Task<List<ProductFeatureResponse>> GetFeaturedAsync(int take = 8);
        Task<ProductDTO?> GetByIdAsync(string id);
        Task<ProductDetailResponse?> GetDetails(string id);
        Task<List<ProductDTO>> GetBySlugAsync(string slug);
        Task<ProductDTO> CreateAsync(ProductCreateRequest request);
        Task<ProductDTO> UpdateAsync(string id, ProductUpdateRequest request);
        Task DeleteAsync(string id);
    }
}

