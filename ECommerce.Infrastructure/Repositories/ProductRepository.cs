using ECommerce.Application.Domain.Interfaces;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Interfaces;
using ECommerce.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _context;

    public ProductRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public IUnitOfWork UnitOfWork => _context;

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await _context.Products
                             .Include(p => p.Categories)
                             .ToListAsync();
    }

    public async Task<Product?> GetByIdAsync(string id)
    {
        return await _context.Products
                             .Include(p => p.Categories)
                             .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IEnumerable<Product>> GetByCategoryAsync(string categoryId)
    {
        return await _context.Products
                             .Where(p => p.CategoryId == categoryId)
                             .Include(p => p.Categories)
                             .ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetProductsBySlug(string slug)
    {
        return await _context.Products
                            .Where(p => p.Slug == slug)
                            .Include(p => p.Categories)
                            .ToListAsync();
    }
}
