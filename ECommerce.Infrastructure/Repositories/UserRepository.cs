using ECommerce.Domain.Entities.ApplicationUser;
using ECommerce.Domain.Interfaces;
using ECommerce.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;

    public IUnitOfWork UnitOfWork => _context;

    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddUserAsync(User user)
    {
        await _context.Users.AddAsync(user);
    }

    public async Task DeleteUserAsync(string id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user != null)
        {
            _context.Users.Remove(user);
        }
    }

    public Task<bool> EmailExistsAsync(string email)
    {
        return _context.Users.AnyAsync(u => u.Email == email);
    }

    public Task<User?> GetUserByRefreshTokenAsync(string refreshToken)
    {
        return _context.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
    }

    public async Task UpdateRefreshTokenAsync(string userId, string newRefreshToken, DateTime refreshTokenExpiryTime)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user != null)
        {
            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiryTime = refreshTokenExpiryTime;
        }
    }

    public Task<User?> GetUserByEmailAsync(string email)
    {
        return _context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task UpdateUserAsync(User user)
    {
        var existingUser = await _context.Users.FindAsync(user.Id);
        if (existingUser != null)
        {
            _context.Entry(existingUser).CurrentValues.SetValues(user);
        }
    }

    public Task<User?> GetUserByIdAsync(string id)
    {
        return _context.Users.FirstOrDefaultAsync(u => u.Id == id);
    }
}