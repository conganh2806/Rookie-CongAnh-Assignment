using ECommerce.Application.Domain.Interfaces;

namespace ECommerce.Domain.Interfaces 
{ 
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository ProductRepository { get; }
        Task<int> SaveChangesAsync();
    }
}