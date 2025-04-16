using System.Security.Claims;
using ECommerce.Application.DTOs;
using ECommerce.Application.DTOs.Response;
using ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities.ApplicationUser;
using ECommerce.Domain.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Application.Services.Authentication
{
    public class CookieAuthService : ICookieAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CookieAuthService(
            IUserRepository userRepository,
            IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<CookieAuthResponse?> RegisterAsync(RegisterRequest request)
        {
            if (await _userRepository.Entity.AnyAsync(u => u.Email == request.Email))
                return null;

            var user = new User
            {
                Email = request.Email,
                PasswordHash = HashPassword(request.Password),
                FirstName = request.FirstName,
                LastName = request.LastName
            };

            _userRepository.Add(user);
            await _userRepository.UnitOfWork.SaveChangesAsync();

            await SignInWithCookieAsync(user);

            return new CookieAuthResponse
            {
                UserId    = user.Id,
                Email     = user.Email,
                FirstName = user.FirstName!,
                LastName  = user.LastName!
            };
        }

        public async Task<CookieAuthResponse?> LoginAsync(LoginRequest request)
        {
            var user = await _userRepository.Entity
                .Where(u => u.Email == request.Email)
                .FirstOrDefaultAsync();

            if (user == null || !PasswordMatches(request.Password, user.PasswordHash!))
            {
                return null;
            }
            
            await SignInWithCookieAsync(user);

            return new CookieAuthResponse
            {
                UserId = user.Id,
                Email = user.Email ?? default!,
                FirstName = user.FirstName!,
                LastName = user.LastName!
            };
        }

        public async Task LogoutAsync()
        {
            var context = _httpContextAccessor.HttpContext;
            if (context != null)
                await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        public Task<bool> IsSignedInAsync()
        {
            var user = _httpContextAccessor.HttpContext?.User;
            return Task.FromResult(user?.Identity?.IsAuthenticated ?? false);
        }

        private async Task SignInWithCookieAsync(User user)
        {
            var context = _httpContextAccessor.HttpContext;
            if (context == null) return;

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.GivenName, user.FirstName ?? string.Empty),
                new Claim(ClaimTypes.Surname, user.LastName ?? string.Empty)
            };

            var identity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme); 
            var principal = new ClaimsPrincipal(identity);

            System.Console.WriteLine($"[CookieAuthService] SignInWithCookieAsync: ");
            
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc   = DateTimeOffset.UtcNow.AddDays(7),
                AllowRefresh = true
            };

            await context.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal,
                authProperties
            );
        }


        private bool PasswordMatches(string password, string hashedPassword)
            => BCrypt.Net.BCrypt.Verify(password, hashedPassword);
          
        private string HashPassword(string password)
            => BCrypt.Net.BCrypt.HashPassword(password);

        
        
    }
}
