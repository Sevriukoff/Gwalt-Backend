using Sevriukoff.Gwalt.Infrastructure.Entities;

namespace Sevriukoff.Gwalt.Infrastructure.Caching;

public class LikeCacheClient
{
    private readonly ICacheProvider _cacheProvider;
    private readonly ICacheKeyManager _keyManager;

    public LikeCacheClient(ICacheProvider cacheProvider, ICacheKeyManager keyManager)
    {
        _cacheProvider = cacheProvider;
        _keyManager = keyManager;
    }
    
    public Task IncrementTrackLikesAsync(int trackId)
    {
        var key = _keyManager.GetTrackLikesCountKey(trackId);
        return _cacheProvider.IncrementCountAsync(key);
    }
    
    public Task DecrementTrackLikesAsync(int trackId)
    {
        var key = _keyManager.GetTrackLikesCountKey(trackId);
        return _cacheProvider.DecrementCountAsync(key);
    }
    
    public Task IncrementAlbumLikesAsync(int albumId)
    {
        var key = _keyManager.GetAlbumLikesCountKey(albumId);
        return _cacheProvider.IncrementCountAsync(key);
    }
    
    public Task DecrementAlbumLikesAsync(int albumId)
    {
        var key = _keyManager.GetAlbumLikesCountKey(albumId);
        return _cacheProvider.DecrementCountAsync(key);
    }
    
    public Task IncrementUserLikeAsync(int userId)
    {
        var key = _keyManager.GetUserLikesCountKey(userId);
        return _cacheProvider.IncrementCountAsync(key);
    }
    
    public Task DecrementUserLikeAsync(int userId)
    {
        var key = _keyManager.GetUserLikesCountKey(userId);
        return _cacheProvider.DecrementCountAsync(key);
    }
    
    public Task<int> GetLikesCountByUserAsync(int userId)
        => _cacheProvider.GetValueAsync<int>(_keyManager.GetUserLikesCountKey(userId));
    
    public Task<int> GetLikesCountByTrackAsync(int trackId)
        => _cacheProvider.GetValueAsync<int>(_keyManager.GetTrackLikesCountKey(trackId));
    
    public Task<int> GetLikesCountByAlbumAsync(int albumId)
        => _cacheProvider.GetValueAsync<int>(_keyManager.GetAlbumLikesCountKey(albumId));
    
    public Task<Dictionary<int, int>> GetTrackLikesCountsAsync()
        => _cacheProvider.GetCountsAsync(_keyManager.InstanceName, typeof(Track), CacheKeyType.LikesCount);

    public Task<Dictionary<int, int>> GetAlbumLikesCountsAsync()
        => _cacheProvider.GetCountsAsync(_keyManager.InstanceName, typeof(Album), CacheKeyType.LikesCount);
    
    public Task<Dictionary<int, int>> GetUserLikesCountsAsync()
        => _cacheProvider.GetCountsAsync(_keyManager.InstanceName, typeof(User), CacheKeyType.LikesCount);
    

    public Task ClearTrackLikesCountsAsync()
        => _cacheProvider.ClearCountsAsync(_keyManager.InstanceName, typeof(Track), CacheKeyType.LikesCount);

    public Task ClearAlbumLikesCountsAsync()
        => _cacheProvider.ClearCountsAsync(_keyManager.InstanceName, typeof(Album), CacheKeyType.LikesCount);
    
    public Task ClearUserLikesCountsAsync()
        => _cacheProvider.ClearCountsAsync(_keyManager.InstanceName, typeof(User), CacheKeyType.LikesCount);
}