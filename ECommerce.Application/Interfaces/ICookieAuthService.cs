using System.Security.Claims;
using ECommerce.Application.DTOs.Response;

namespace ECommerce.Application.Interfaces
{
    public interface ICookieAuthService : IAuthService<CookieAuthResponse>
    {
        Task LogoutAsync();
        Task<bool> IsSignedInAsync();
    }
}