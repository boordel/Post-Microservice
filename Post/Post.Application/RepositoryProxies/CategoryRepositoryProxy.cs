using Post.Domain.Entities.CategoryAggregate;
using System;

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

    public async Task<IEnumerable<Category>?> GetCategoriesAsync()
    {
        //return await _cacheService.GetOrCreateAsync(
        //    CacheSettings.Key_CategoryList,
        //    _categoryRepository.GetAllAsync,
        //    TimeSpan.FromMinutes(CacheSettings.CacheValidationMinutes));

        // For testable code, we must use normal get and set functions of cache service
        var cachedValue = await _cacheService.GetAsync<IEnumerable<Category>>(CacheSettings.Key_CategoryList);

        if (cachedValue != null)
            return cachedValue;

        cachedValue = await _categoryRepository.GetAllAsync();

        if (cachedValue != null)
            await _cacheService.SetAsync(
                CacheSettings.Key_CategoryList, 
                cachedValue,
                TimeSpan.FromMinutes(CacheSettings.CacheValidationMinutes));

        return cachedValue;
    }
}
