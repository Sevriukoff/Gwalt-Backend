using Sevriukoff.Gwalt.Infrastructure.Entities;

namespace Sevriukoff.Gwalt.Infrastructure.Caching;

//Класс не нуждается в интерфейсе, т.к. является просто декаратором.
public class ListenCacheClient
{
    private readonly ICacheProvider _cacheProvider;
    private readonly ICacheKeyManager _keyManager;

    public ListenCacheClient(ICacheProvider cacheProvider, ICacheKeyManager keyManager)
    {
        _cacheProvider = cacheProvider;
        _keyManager = keyManager;
    }
    
    public Task IncrementTrackListensAsync(int trackId)
    {
        var key = _keyManager.GetTrackListensCountKey(trackId);
        return _cacheProvider.IncrementCountAsync(key);
    }
    
    public Task IncrementAlbumListensAsync(int albumId)
    {
        var key = _keyManager.GetAlbumListensCountKey(albumId);
        return _cacheProvider.IncrementCountAsync(key);
    }
    
    public Task IncrementUserListensAsync(int userId)
    {
        var key = _keyManager.GetUserListensCountKey(userId);
        return _cacheProvider.IncrementCountAsync(key);
    }
    
    public Task<int> GetListensCountByUserAsync(int userId)
        => _cacheProvider.GetValueAsync<int>(_keyManager.GetUserListensCountKey(userId));
    
    public Task<int> GetListensCountByTrackAsync(int trackId)
        => _cacheProvider.GetValueAsync<int>(_keyManager.GetTrackListensCountKey(trackId));
    
    public Task<int> GetListensCountByAlbumAsync(int albumId)
        => _cacheProvider.GetValueAsync<int>(_keyManager.GetAlbumListensCountKey(albumId));
    
    public Task<Dictionary<int, int>> GetTrackListensCountsAsync()
        => _cacheProvider.GetCountsAsync(_keyManager.InstanceName, typeof(Track), CacheKeyType.ListensCount);

    public Task<Dictionary<int, int>> GetAlbumListensCountsAsync()
        => _cacheProvider.GetCountsAsync(_keyManager.InstanceName, typeof(Album), CacheKeyType.ListensCount);
    
    public Task<Dictionary<int, int>> GetUserListensCountsAsync()
        => _cacheProvider.GetCountsAsync(_keyManager.InstanceName, typeof(User), CacheKeyType.ListensCount);
    

    public Task ClearTrackListensCountsAsync()
        => _cacheProvider.ClearCountsAsync(_keyManager.InstanceName, typeof(Track), CacheKeyType.ListensCount);

    public Task ClearAlbumListensCountsAsync()
        => _cacheProvider.ClearCountsAsync(_keyManager.InstanceName, typeof(Album), CacheKeyType.ListensCount);
    
    public Task ClearUserListensCountsAsync()
        => _cacheProvider.ClearCountsAsync(_keyManager.InstanceName, typeof(User), CacheKeyType.ListensCount);
}