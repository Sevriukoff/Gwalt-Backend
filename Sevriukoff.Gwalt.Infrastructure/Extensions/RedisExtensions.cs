using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;

namespace Sevriukoff.Gwalt.Infrastructure.Extensions;

public static class RedisExtensions
{
    public static async Task<string?> GetStringExAsync(this IDistributedCache cache, object key)
    {
        return await cache.GetStringAsync(GetString(key));
    }
    
    public static async Task SetStringExAsync(this IDistributedCache cache, object key, string value, DistributedCacheEntryOptions? options = null)
    {
        await cache.SetStringAsync(GetString(key), value);
    }
    
    public static async Task SetStringExAsync(this IDistributedCache cache, object key, object value, DistributedCacheEntryOptions? options = null)
    {
        await cache.SetStringAsync(GetString(key), GetString(value));
    }
    
    public static async Task RemoveExAsync(this IDistributedCache cache, object key)
    {
        await cache.RemoveAsync(GetString(key));
    }
    
    public static async Task SetObjectExAsync<T>(this IDistributedCache cache, object key, T value, DistributedCacheEntryOptions? options = null)
    {
        string serializedValue = JsonSerializer.Serialize(value);
        await cache.SetStringExAsync(key, serializedValue, options);
    }
    
    public static async Task SetObjectAsync<T>(this IDistributedCache cache, string key, T value, DistributedCacheEntryOptions? options = null)
    {
        string serializedValue = JsonSerializer.Serialize(value);
        await cache.SetStringAsync(key, serializedValue, options);
    }

    public static async Task<T?> GetObjectExAsync<T>(this IDistributedCache cache, object key)
    {
        var serializedValue = await cache.GetStringExAsync(key);
        return serializedValue != null ? JsonSerializer.Deserialize<T>(serializedValue) : default;
    }
    
    public static async Task<T?> GetObjectAsync<T>(this IDistributedCache cache, string key)
    {
        var serializedValue = await cache.GetStringAsync(key);
        return serializedValue != null ? JsonSerializer.Deserialize<T>(serializedValue) : default;
    }
    
    private static string GetString(object? key)
    {
        return key?.ToString() ?? throw new ArgumentNullException(nameof(key));
    }
}