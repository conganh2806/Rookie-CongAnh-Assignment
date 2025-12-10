using System.Security.Claims;
using ECommerce.Application.Interfaces;
using Microsoft.AspNetCore.Http;

namespace ECommerce.Infrastructure.Identity
{
    public class CurrentUser : ICurrentUser
    {
        private readonly IHttpContextAccessor _context;

        public CurrentUser(IHttpContextAccessor context)
        {
            _context = context;
        }

        public string? UserId =>
            _context.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        public string? Email => _context.HttpContext?.User?.FindFirstValue(ClaimTypes.Email);
        public IReadOnlyList<string> Roles =>
            _context.HttpContext?.User?.FindAll(ClaimTypes.Role).Select(r => r.Value).ToList()
            ?? new List<string>();
    }
}
