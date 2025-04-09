namespace ECommerce.Domain.Interfaces 
{ 
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync();
    }
}