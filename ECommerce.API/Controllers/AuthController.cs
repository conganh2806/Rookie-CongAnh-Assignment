using System.Net;
using ECommerce.Application.Common.Utilities;
using ECommerce.Application.Common.Utilities.Exceptions;
using ECommerce.Application.DTOs;
using ECommerce.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
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
                ? Error(Constants.REGISTER_ERROR, HttpStatusCode.Conflict)
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
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
        {
            var result = await _jwtService.RefreshTokenAsync(request);
            if (result == null) 
            {
                throw new UnAuthorizedException("Refresh token not valid or expired!");
            }

            return Success(result);
        }
    }
}
