namespace Post.Domain.SeedWorks;
public interface IRepository<T> where T : IAggregateRoot
{
    Task<T?> Get(int id);
    Task<IEnumerable<T>> GetAll();
    T Insert(T entity);
    T Update(T entity);
    Task Delete(int id);
}
