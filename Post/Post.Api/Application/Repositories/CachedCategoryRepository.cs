using Microsoft.Extensions.Caching.Memory;
using Post.Domain.Entities.CategoryAggregate;

namespace Post.Api.Application.Repositories;

public class CachedCategoryRepository
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMemoryCache _memoryCache;

    private const string CacheKey_CategoryList = "ctg_list";
    private const int CacheValidationMinutes = 5;

    public CachedCategoryRepository(ICategoryRepository categoryRepository, IMemoryCache memoryCache)
    {
        _categoryRepository = categoryRepository;
        _memoryCache = memoryCache;
    }

    public async Task<IEnumerable<Category>?> GetCategoriesAsync()
    {
        return await _memoryCache.GetOrCreateAsync<IEnumerable<Category>>(
            CacheKey_CategoryList,
            async entry =>
            {
                entry.SlidingExpiration = TimeSpan.FromMinutes(CacheValidationMinutes);
                return await _categoryRepository.GetAllAsync();
            });
    }
}
