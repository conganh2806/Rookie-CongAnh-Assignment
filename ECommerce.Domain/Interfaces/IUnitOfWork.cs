using ECommerce.Application.Domain.Interfaces;

namespace ECommerce.Domain.Interfaces 
{ 
    public interface IUnitOfWork : IDisposable
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}