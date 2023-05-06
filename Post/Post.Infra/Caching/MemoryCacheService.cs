using Microsoft.Extensions.Caching.Memory;
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

    public async Task<T?> GetAsync<T>(string cacheKey) where T : class
    {
        ConcurrentDictionary<string, bool> list = new();
        list.TryAdd(cacheKey, false);

        IEnumerable<Task<T?>> tasks = (IEnumerable<Task<T?>>)list
            .Keys
            .Select(x => _memoryCache.Get<T>(x));

        var result = await Task.WhenAll(tasks);

        return result.Count() > 0 ? result[0] : null;
    }

    public Task SetAsync<T>(string cacheKey, T value, TimeSpan timeSpan = default) where T : class
    {
        ConcurrentDictionary<string, bool> list = new();
        list.TryAdd(cacheKey, false);

        IEnumerable<Task> tasks = (IEnumerable<Task>)list
            .Keys
            .Select(x => timeSpan != default ?
                _memoryCache.Set<T>(x, value, timeSpan) :
                _memoryCache.Set<T>(x, value));

        CacheKeys.TryAdd(cacheKey, false);

        return Task.WhenAll(tasks);
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

    private object? Remove(object cacheKey)
    {
        var value = _memoryCache.Get(cacheKey);

        _memoryCache.Remove(cacheKey);

        CacheKeys.TryRemove((string)cacheKey, out bool _);

        return value;
    }

    public Task RemoveAsync(string cacheKey)
    {
        ConcurrentDictionary<string, bool> list = new();
        list.TryAdd(cacheKey, false);

        IEnumerable<Task> tasks = (IEnumerable<Task>)list
            .Keys
            .Select(x => Remove(x));

        return Task.WhenAll(tasks);
    }

    public Task RemoveByPerfixAsync(string perfixKey)
    {
        IEnumerable<Task> tasks = (IEnumerable<Task>)CacheKeys
            .Keys
            .Where(x => x.StartsWith(perfixKey))
            .Select(x => Remove(x));

        return Task.WhenAll(tasks);
    }
}
