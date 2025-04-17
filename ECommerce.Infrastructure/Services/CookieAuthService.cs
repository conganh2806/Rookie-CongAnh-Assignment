using ECommerce.Application.DTOs;
using ECommerce.Application.DTOs.Response;
using ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities.ApplicationUser;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace ECommerce.Infrastructure.Services
{
    public class CookieAuthService : ICookieAuthService
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _contextAccessor;

        public CookieAuthService(SignInManager<User> signInManager,
                                UserManager<User> userManager,
                                IHttpContextAccessor contextAccessor)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _contextAccessor = contextAccessor;
        }

        public async Task<CookieAuthResponse?> LoginAsync(LoginRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null) return null;

            var isValid = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!isValid) return null;

            await _signInManager.SignInAsync(user, isPersistent: true);

            return new CookieAuthResponse
            {
                UserId = user.Id,
                Email = user.Email!,
                FirstName = user.FirstName ?? string.Empty,
                LastName = user.LastName ?? string.Empty
            };
        }

        public async Task<CookieAuthResponse?> RegisterAsync(RegisterRequest request)
        {
            var user = new User
            {
                UserName = request.Email,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
                return null;

            await _signInManager.SignInAsync(user, isPersistent: true);

            return new CookieAuthResponse
            {
                UserId = user.Id,
                Email = user.Email!,
                FirstName = user.FirstName,
                LastName = user.LastName
            };
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public Task<bool> IsSignedInAsync()
        {
            var user = _contextAccessor.HttpContext?.User;
            return Task.FromResult(user?.Identity?.IsAuthenticated ?? false);
        }
    }
}
