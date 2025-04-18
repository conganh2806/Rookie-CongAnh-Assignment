using System.Security.Claims;
using ECommerce.Application.DTOs;
using ECommerce.Application.DTOs.Response;

namespace ECommerce.Application.Interfaces
{
    public interface ICookieAuthService : IAuthService<CookieAuthResponse>
    {
        Task<CookieAuthResponse?> RegisterAsync(RegisterRequest request);
        Task LogoutAsync();
        Task<bool> IsSignedInAsync();
    }
}