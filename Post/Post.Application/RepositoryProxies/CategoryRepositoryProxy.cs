using Post.Domain.Entities.CategoryAggregate;

namespace Post.Application.Repositories;

public class CategoryRepositoryProxy: ICategoryRepositoryProxy
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly ICacheService _cacheService;

    public CategoryRepositoryProxy(ICategoryRepository categoryRepository, ICacheService cacheService)
    {
        _categoryRepository = categoryRepository;
        _cacheService = cacheService;
    }

    public async Task<IEnumerable<Category>?> GetCategoriesAsync() =>
        await _cacheService.GetOrCreateAsync(
            CacheSettings.Key_CategoryList,
            _categoryRepository.GetAllAsync,
            TimeSpan.FromMinutes(CacheSettings.CacheValidationMinutes));
}
