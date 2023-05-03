namespace Post.Domain.SeedWorks;
public interface IRepository<T> where T : IAggregateRoot
{
    Task<T?> Get(int id);
    Task<IEnumerable<T>> GetAll();
    Task Insert(T entity);
    Task Update(T entity);
    Task Delete(int id);
}
