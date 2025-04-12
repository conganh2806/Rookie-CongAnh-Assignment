using System.Linq.Expressions;
using ECommerce.Domain.Interfaces;

public interface IGenericRepository<T> where T : class
{
    T? GetById(string id);
    IEnumerable<T> GetAll();
    IEnumerable<T> Find(Expression<Func<T, bool>> expression);
    void Add(T entity);
    void AddRange(IEnumerable<T> entities);
    void Remove(T entity);
    void RemoveRange(IEnumerable<T> entities);
    IUnitOfWork UnitOfWork { get; }
}