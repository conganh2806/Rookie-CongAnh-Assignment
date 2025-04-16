using ECommerce.Application.DTOs;
using ECommerce.Application.DTOs.Request;

namespace ECommerce.Application.Services
{
    public interface IOrderService
    {
        Task<List<OrderDto>> GetAllAsync();
        Task<OrderDto?> GetByIdAsync(string id);
        Task<OrderDto> CreateAsync(OrderCreateRequest request);
        Task<OrderDto> UpdateAsync(string id, OrderUpdateRequest request);
        Task DeleteAsync(string id);
    }
}