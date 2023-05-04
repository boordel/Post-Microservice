using Microsoft.Extensions.Caching.Memory;
using Post.Api.Application.Settings;
using Post.Domain.Entities.CategoryAggregate;

namespace Post.Api.Application.Repositories;

public class CategoryRepositoryMemoryCache
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMemoryCache _memoryCache;

    public CategoryRepositoryMemoryCache(ICategoryRepository categoryRepository, IMemoryCache memoryCache)
    {
        _categoryRepository = categoryRepository;
        _memoryCache = memoryCache;
    }

    public async Task<IEnumerable<Category>?> GetCategoriesAsync() =>
        await _memoryCache.GetOrCreateAsync(
            CacheSettings.Key_CategoryList,
            entry =>
            {
                entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(CacheSettings.CacheValidationMinutes));
                return _categoryRepository.GetAllAsync();
            });
}
