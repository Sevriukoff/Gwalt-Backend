namespace Sevriukoff.Gwalt.Infrastructure.Caching;

public interface ICacheProvider
{
    Task<T?> GetValueAsync<T>(CacheKey cacheKey);
    
    Task IncrementCountAsync(CacheKey key);
    Task DecrementCountAsync(CacheKey key);
    
    Task<Dictionary<int, int>> GetCountsAsync(string instanceName, Type entityType, CacheKeyType? keyType = null);
    Task ClearCountsAsync(string instanceName, Type entityType, CacheKeyType? keyType = null);
}