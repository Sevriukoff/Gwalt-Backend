namespace Sevriukoff.Gwalt.Infrastructure.Caching;

public interface ICacheKeyManager
{
    public string InstanceName { get; }
    
    CacheKey GetTrackListensCountKey(int trackId);
    CacheKey GetAlbumListensCountKey(int albumId);
    CacheKey GetUserListensCountKey(int userId);
    
    CacheKey GetTrackLikesCountKey(int trackId);
    CacheKey GetAlbumLikesCountKey(int albumId);
    CacheKey GetUserLikesCountKey(int userId);
    
    CacheKey GetUserFollowersCountKey(int userId);
    CacheKey GetUserFollowingsCountKey(int userId);
}