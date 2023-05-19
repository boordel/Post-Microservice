using Microsoft.Extensions.Caching.Memory;
using Post.Application.Contracts;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Infra.Caching;
public class MemoryCacheService : ICacheService
{
    private readonly IMemoryCache _memoryCache;

    public MemoryCacheService(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }

    private ConcurrentDictionary<string, bool> CacheKeys = new();

    public async Task<T?> GetAsync<T>(string cacheKey) where T : class =>
        await Task.FromResult(_memoryCache.Get<T>(cacheKey));


    public Task SetAsync<T>(string cacheKey, T value, TimeSpan timeSpan = default) where T : class
    {
        CacheKeys.TryAdd(cacheKey, false);

        return Task.FromResult(timeSpan != default ?
            _memoryCache.Set<T>(cacheKey, value, timeSpan) :
            _memoryCache.Set<T>(cacheKey, value));
    }

    public async Task<T?> GetOrCreateAsync<T>(string cacheKey, Func<Task<T?>> factory, TimeSpan timeSpan = default) where T : class =>
        await _memoryCache.GetOrCreateAsync(
            cacheKey,
            async entry =>
            {
                if (timeSpan != default)
                    entry.SetAbsoluteExpiration(timeSpan);
                return await factory();
            });

    public Task RemoveAsync(string cacheKey)
    {
        _memoryCache.Remove(cacheKey);

        return Task.FromResult(() => _memoryCache.Remove(cacheKey));
    }

    public Task RemoveByPerfixAsync(string perfixKey)
    {
        IEnumerable<Task> tasks = (IEnumerable<Task>)CacheKeys
            .Keys
            .Where(x => x.StartsWith(perfixKey))
            .Select(x => RemoveAsync(x));

        return Task.WhenAll(tasks);
    }
}
