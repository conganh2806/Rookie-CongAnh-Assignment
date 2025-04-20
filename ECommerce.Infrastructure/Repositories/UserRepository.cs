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

    public IQueryable<User> Entity => _context.Users.AsQueryable();

    public void Add(User user)
    {
        System.Console.WriteLine($"Add: {user.LastName}");
        _context.Users.Add(user);
    }

    public void Update(User user)
    {
        _context.Users.Update(user);
    }

    public void Delete(User user)
    {
        _context.Users.Remove(user);
    }
}