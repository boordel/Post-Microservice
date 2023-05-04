using Microsoft.Extensions.Caching.Distributed;
using Post.Api.Application.Settings;
using Post.Domain.Entities.CategoryAggregate;
using System.Text.Json;

namespace Post.Api.Application.Repositories;

public class CategoryRepositoryRedisCache
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IDistributedCache _distributedCache;

    public CategoryRepositoryRedisCache(ICategoryRepository categoryRepository, IDistributedCache distributedCache)
    {
        _categoryRepository = categoryRepository;
        _distributedCache = distributedCache;
    }

    public async Task<IEnumerable<Category>?> GetCategoriesAsync()
    {
        var categoriesString = await _distributedCache.GetStringAsync(CacheSettings.Key_CategoryList);

        IEnumerable<Category>? categories;
        if (string.IsNullOrEmpty(categoriesString))
        {
            categories = await _categoryRepository.GetAllAsync();

            if (categories is null)
                return categories;

            await _distributedCache.SetStringAsync(
                CacheSettings.Key_CategoryList,
                JsonSerializer.Serialize(categories),
                new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(CacheSettings.CacheValidationMinutes))
                );

            return categories;
        }

        categories = JsonSerializer.Deserialize<IEnumerable<Category>>(categoriesString);
        return categories;
    }
}
