using ECommerce.Application.DTOs.Response;

namespace ECommerce.Application.DTOs
{
    public class JWTAuthResponse : IAuthResponse
    {
        public string Token { get; set; } = default!;
        public string RefreshToken { get; set; } = default!;
        public DateTime Expiration { get; set; }
    }
}