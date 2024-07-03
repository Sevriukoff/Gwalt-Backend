using Sevriukoff.Gwalt.Infrastructure.Entities;

namespace Sevriukoff.Gwalt.Infrastructure.Caching;

//Класс не нуждается в интерфейсе, т.к. является просто декаратором.
public class FollowerCacheClient
{
    private readonly ICacheProvider _cacheProvider;
    private readonly ICacheKeyManager _keyManager;

    public FollowerCacheClient(ICacheProvider cacheProvider, ICacheKeyManager keyManager)
    {
        _cacheProvider = cacheProvider;
        _keyManager = keyManager;
    }

    public async Task IncrementFollowersAsync(int userId, int followerId)
    {
        var followerKey = _keyManager.GetUserFollowersCountKey(userId);
        var followingKey = _keyManager.GetUserFollowingsCountKey(followerId);

        await _cacheProvider.IncrementCountAsync(followerKey);
        await _cacheProvider.IncrementCountAsync(followingKey);
    }
    
    public async Task DecrementFollowersAsync(int userId, int followerId)
    {
        var followerKey = _keyManager.GetUserFollowersCountKey(userId);
        var followingKey = _keyManager.GetUserFollowingsCountKey(followerId);
        
        await _cacheProvider.DecrementCountAsync(followerKey);
        await _cacheProvider.DecrementCountAsync(followingKey);
    }
    
    public Task<int> GetFollowersCountByUserAsync(int userId)
        => _cacheProvider.GetValueAsync<int>(_keyManager.GetUserFollowersCountKey(userId));
    
    public Task<int> GetFollowingsCountByUserAsync(int userId)
        => _cacheProvider.GetValueAsync<int>(_keyManager.GetUserFollowingsCountKey(userId));

    public Task<Dictionary<int, int>> GetFollowersCountAsync()
        => _cacheProvider.GetCountsAsync(_keyManager.InstanceName, typeof(User), CacheKeyType.FollowersCount);
    
    public Task<Dictionary<int, int>> GetFollowingsCountAsync()
        => _cacheProvider.GetCountsAsync(_keyManager.InstanceName, typeof(User), CacheKeyType.FollowingsCount);

    public Task ClearFollowersCountAsync()
        => _cacheProvider.ClearCountsAsync(_keyManager.InstanceName, typeof(User), CacheKeyType.FollowersCount);
    
    public Task ClearFollowingsCountAsync()
        => _cacheProvider.ClearCountsAsync(_keyManager.InstanceName, typeof(User), CacheKeyType.FollowingsCount);
}