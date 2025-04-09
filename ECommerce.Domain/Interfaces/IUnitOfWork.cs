using System.Threading.Tasks;

namespace ECommerce.Domain.Interfaces 
{ 
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync();
    }
}