using ECommerce.Application.DTOs;
using ECommerce.Application.DTOs.Request;
using ECommerce.Application.DTOs.Response;
using ECommerce.Application.Entities.ApplicationUser;

namespace ECommerce.Application.Interfaces
{
    public interface IAuthService<TResponse>
        where TResponse : IAuthResponse
    {
        Task<TResponse?> LoginAsync(LoginRequest request);
        Task<GetMeResponse?> GetMeAsync();
    }
}
