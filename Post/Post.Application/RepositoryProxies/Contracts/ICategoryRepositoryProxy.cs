using Post.Domain.Entities.CategoryAggregate;

namespace Post.Application.RepositoryProxies.Contracts;
public interface ICategoryRepositoryProxy
{
    Task<IEnumerable<Category>?> GetCategoriesAsync();
}
