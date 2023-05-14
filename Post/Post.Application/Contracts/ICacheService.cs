namespace Post.Application.Contracts;

public interface ICacheService
{
    Task<T?> GetAsync<T>(string cacheKey) where T : class;
    Task SetAsync<T>(string cacheKey, T value, TimeSpan timeSpan = default) where T : class;
    Task<T?> GetOrCreateAsync<T>(string cacheKey, Func<Task<T?>> factory, TimeSpan timeSpan = default) where T : class;
    Task RemoveAsync(string cacheKey);
    Task RemoveByPerfixAsync(string perfixKey);
}
