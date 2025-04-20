using ECommerce.Application.DTOs;

namespace ECommerce.Application.Services
{
    public interface IUserService
    {
        Task<List<UserDto>> GetAllAsync();
        Task<UserDto?> GetByIdAsync(string id);
        Task<UserDto> UpdateAsync(string id, UserUpdateRequest request);
        Task DeleteAsync(string id);
    }
}