using ECommerce.Application.DTOs;

namespace ECommerce.Application.Interfaces
{
    public interface IJWTAuthService : IAuthService<JWTAuthResponse>
    {
        Task<JWTAuthResponse?> RefreshTokenAsync(string refreshToken);
    }
}
