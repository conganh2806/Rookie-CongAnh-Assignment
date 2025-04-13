using ECommerce.Domain.Interfaces;

public interface IGenericRepository<T> where T : class
{
    IQueryable<T> Ts { get; }
    void Add(T entity);
    void Update(T entity);
    void Delete(T entity);
    IUnitOfWork UnitOfWork { get; }
}