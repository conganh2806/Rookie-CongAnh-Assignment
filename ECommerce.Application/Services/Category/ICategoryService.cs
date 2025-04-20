using ECommerce.Application.DTOs;

namespace ECommerce.Application.Services
{
    public interface ICategoryService
    {
        Task<List<CategoryDto>> GetAllAsync();
        Task<CategoryDto?> GetByIdAsync(string id);
        Task<CategoryDto?> GetBySlugAsync(string slug);
        Task<CategoryDto> CreateAsync(CategoryCreateRequest request);
        Task<CategoryDto> UpdateAsync(string id, CategoryUpdateRequest request);
        Task DeleteAsync(string id);
    }
}