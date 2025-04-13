using ECommerce.Application.DTOs;

namespace ECommerce.Application.Interfaces
{
    public interface IRedisCartService
    {
        Task<CartDto> GetCartAsync(string userId);
        Task<CartDto> AddOrUpdateItemAsync(string userId, CartItemDto item);
        Task<bool> RemoveItemAsync(string userId, string productId);
        Task<bool> ClearCartAsync(string userId);
    }
}
