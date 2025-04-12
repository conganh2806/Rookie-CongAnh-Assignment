using ECommerce.Domain.Entities;
using ECommerce.Domain.Interfaces;

namespace ECommerce.Application.Domain.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<IEnumerable<Product>> GetByCategoryAsync(string categoryId);
        Task<Product?> GetByIdAsync(string id);
        Task<IEnumerable<Product>> GetProductsBySlug(string slug);
        IUnitOfWork UnitOfWork { get; }
    }   
}