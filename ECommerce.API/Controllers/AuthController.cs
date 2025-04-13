using System.Net;
using ECommerce.Application.Common;
using ECommerce.Application.DTOs;
using ECommerce.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IJWTAuthService _jwtService;

        public AuthController(IJWTAuthService jwtService)
        {
            _jwtService = jwtService;
        }

        [HttpPost("register")]
        public async Task<BaseResponse<JWTAuthResponse>> Register([FromBody] RegisterRequest request)
        {
            var result = await _jwtService.RegisterAsync(request);

            return result == null
                ? BaseResponse<JWTAuthResponse>
                            .Fail("Email already exists or registration failed.", HttpStatusCode.Conflict)
                : BaseResponse<JWTAuthResponse>.Success(result, "Sign up successfully");
        }

        [HttpPost("login")]
        public async Task<BaseResponse<JWTAuthResponse>> Login([FromBody] LoginRequest request)
        {
            var result = await _jwtService.LoginAsync(request);
            return result == null 
                ? BaseResponse<JWTAuthResponse>
                                .Fail("Invalid email or password", HttpStatusCode.Unauthorized)
                : BaseResponse<JWTAuthResponse>.Success(result, "Login Successfully");
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] string refreshToken)
        {
            var result = await _jwtService.RefreshTokenAsync(refreshToken);
            if (result == null) return Unauthorized("Invalid refresh token.");
            return Ok(result);
        }
    }
}
