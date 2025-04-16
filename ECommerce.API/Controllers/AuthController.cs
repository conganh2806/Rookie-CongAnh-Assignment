using System.Net;
using ECommerce.Application.Common.Utilities;
using ECommerce.Application.DTOs;
using ECommerce.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
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
                ? Error(Constants.REGISTER_SUCCESS, HttpStatusCode.Conflict)
                : Success(result, Constants.REGISTER_SUCCESS);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var result = await _jwtService.LoginAsync(request);
            return result == null 
                ? Error(Constants.LOGIN_ERROR, HttpStatusCode.Unauthorized)
                : Success(result, Constants.LOGIN_SUCCESS);
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] string refreshToken)
        {
            var result = await _jwtService.RefreshTokenAsync(refreshToken);
            if (result == null) return Unauthorized(Constants.REFRESH_TOKEN_INVALID);
            return Ok(result);
        }
    }
}
