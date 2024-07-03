using System.Text.Json;
using StackExchange.Redis;

namespace Sevriukoff.Gwalt.Infrastructure.Caching;

public class RedisCacheProvider : ICacheProvider
{
    private readonly IConnectionMultiplexer _redisConnection;
    private readonly IDatabase _database;

    public RedisCacheProvider(IConnectionMultiplexer redisConnection)
    {
        _redisConnection = redisConnection ?? throw new ArgumentNullException(nameof(redisConnection));
        _database = _redisConnection.GetDatabase();
    }

    public async Task IncrementCountAsync(CacheKey cacheKey)
    {
        var keyStr = cacheKey.GetKey();
        await _database.StringIncrementAsync(keyStr);
    }
    
    public async Task DecrementCountAsync(CacheKey cacheKey)
    {
        var keyStr = cacheKey.GetKey();
        await _database.StringDecrementAsync(keyStr);
    }
    
    public async Task ClearCountsAsync(string instanceName, Type entityType, CacheKeyType? keyType = null)
    {
        var keys = GetKeysByEntity(instanceName, entityType, keyType);
        
        foreach (var key in keys)
            await _database.KeyDeleteAsync(key.GetKey());
    }
    
    public async Task<T?> GetValueAsync<T>(CacheKey cacheKey)
    {
        var keyStr = cacheKey.GetKey();
        var value = await _database.StringGetAsync(keyStr);

        return !value.HasValue ? default : JsonSerializer.Deserialize<T>(value);
    }

    public async Task<Dictionary<int, int>> GetCountsAsync(string instanceName, Type entityType, CacheKeyType? keyType = null)
    {
        var result = new Dictionary<int, int>();
        var keys = GetKeysByEntity(instanceName, entityType, keyType);

        foreach (var key in keys)
        {
            try
            {
                if (!int.TryParse(key.EntityId, out var id))
                    continue;
                
                var count = (int)await _database.StringGetAsync(key.GetKey());
                result[id] = count;
            }
            catch (RedisServerException ex) when (ex.Message.StartsWith("WRONGTYPE"))
            {
                Console.WriteLine($"Key {key} has the wrong type: {ex.Message}");
            }
        }

        return result;
    }

    #region PrivateMethods
    
    private IEnumerable<CacheKey> GetKeysByEntity(string instanceName, Type entityType, CacheKeyType? keyType = null)
    {
        var server = _redisConnection.GetServer(_redisConnection.GetEndPoints().First());
        
        var pattern = keyType == null
            ? $@"\[{instanceName}\]:{entityType.FullName}:*"
            : $@"\[{instanceName}\]:{entityType.FullName}:*:{keyType}";
        var keys = server.Keys(pattern: pattern)
            .Select(x => new CacheKey(x))
            .ToList();

        return keys;
    }
    
    #endregion
}