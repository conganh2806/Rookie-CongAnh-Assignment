using ECommerce.Application.DTOs;

namespace ECommerce.Application.Interfaces
{
    public interface IJWTAuthService : IAuthService<JWTLoginAuthResponse>
    {
        Task<bool?> RegisterAsync(RegisterRequest request);
        Task<JWTLoginAuthResponse?> RefreshTokenAsync(RefreshTokenRequest request);
    }
}
