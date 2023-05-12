using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Post.Infra.Caching;
public class RedisCacheService : ICacheService
{
    private readonly IDistributedCache _distributedCache;

    public RedisCacheService(IDistributedCache distributedCache)
    {
        _distributedCache = distributedCache;
    }

    private ConcurrentDictionary<string, bool> CacheKeys = new();

    public async Task<T?> GetAsync<T>(string cacheKey) where T : class
    {
        var cachedValue = await _distributedCache.GetStringAsync(cacheKey);
        if (string.IsNullOrEmpty(cachedValue))
        {
            return null;
        }

        return JsonSerializer.Deserialize<T>(cachedValue);
    }

    public async Task SetAsync<T>(string cacheKey, T value, TimeSpan timeSpan = default) where T : class
    {
        var options = new DistributedCacheEntryOptions();
        if (timeSpan != default)
            options.SetAbsoluteExpiration(timeSpan);
         
        await _distributedCache.SetStringAsync(
            cacheKey,
            JsonSerializer.Serialize(value),
            options
        );

        CacheKeys.TryAdd(cacheKey, false);
    }

    public async Task<T?> GetOrCreateAsync<T>(string cacheKey, Func<Task<T?>> factory, TimeSpan timeSpan = default) where T : class
    {
        T? cachedValue = await GetAsync<T>(cacheKey);

        if (cachedValue != null)
            return cachedValue;

        cachedValue = await factory();

        if (cachedValue != null)
            await SetAsync(cacheKey, cachedValue, timeSpan);

        return cachedValue;
    }

    public async Task RemoveAsync(string cacheKey)
    {
        await _distributedCache.RemoveAsync(cacheKey);

        CacheKeys.TryRemove(cacheKey, out bool _);
    }

    public Task RemoveByPerfixAsync(string perfixKey)
    {
        IEnumerable<Task> tasks = CacheKeys
            .Keys
            .Where(x => x.StartsWith(perfixKey))
            .Select(x => RemoveAsync(x));

        return Task.WhenAll(tasks);
    }
}
