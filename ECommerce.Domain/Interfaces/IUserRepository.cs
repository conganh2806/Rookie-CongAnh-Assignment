
using ECommerce.Domain.Entities.ApplicationUser;

namespace ECommerce.Domain.Interfaces
{
    public interface IUserRepository
    {
        IQueryable<User> Users { get; }
        void Add(User user);
        void Update(User user);
        void Delete(User user);
        IUnitOfWork UnitOfWork { get; }
    }
}