using ECommerce.Application.DTOs;

namespace ECommerce.Application.Services
{
    public interface ICategoryService
    {
        Task<List<CategoryDto>> GetAllAsync();
    }
}