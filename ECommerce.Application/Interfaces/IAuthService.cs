using ECommerce.Application.DTOs;
using ECommerce.Application.DTOs.Response;

namespace ECommerce.Application.Interfaces
{
    public interface IAuthService<TResponse> 
        where TResponse : IAuthResponse
    {
        Task<TResponse?> LoginAsync(LoginRequest request);
    }

}