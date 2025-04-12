
using ECommerce.Domain.Entities.ApplicationUser;

namespace ECommerce.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> EmailExistsAsync(string email);
        Task<User?> GetUserByEmailAsync(string email);
        Task<User?> GetUserByIdAsync(string id);
        Task<User?> GetUserByRefreshTokenAsync(string refreshToken);
        Task UpdateRefreshTokenAsync(string userId, string newRefreshToken, DateTime refreshTokenExpiryTime);
        Task AddUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(string id);
        IUnitOfWork UnitOfWork { get; }
    }
}