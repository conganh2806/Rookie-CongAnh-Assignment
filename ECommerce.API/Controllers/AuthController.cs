using System.Net;
using ECommerce.Application.Common;
using ECommerce.Application.DTOs;
using ECommerce.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : BaseController<AuthController>
    {
        private readonly IJWTAuthService _jwtService;

        public AuthController(IJWTAuthService jwtService, ILogger<AuthController> logger) 
                : base(logger)
        {
            _jwtService = jwtService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var result = await _jwtService.RegisterAsync(request);

            return result == null
                ? Error("Email already exists or registration failed.", HttpStatusCode.Conflict)
                : Success(result, "Registration successful.");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var result = await _jwtService.LoginAsync(request);
            return result == null 
                ? Error("Invalid email or password.", HttpStatusCode.Unauthorized)
                : Success(result, "Login successful.");
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
