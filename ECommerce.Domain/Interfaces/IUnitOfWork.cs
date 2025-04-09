namespace ECommerce.Domain.Interfaces 
{ 
    public interface IUnitOfWork : IDisposable
    {
        Task<int> SaveChangesAsync();
    }
}