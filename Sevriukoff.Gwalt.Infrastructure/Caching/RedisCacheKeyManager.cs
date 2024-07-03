using Sevriukoff.Gwalt.Infrastructure.Entities;

namespace Sevriukoff.Gwalt.Infrastructure.Caching;

public class RedisCacheKeyManager : ICacheKeyManager
{
    public string InstanceName { get; }

    public RedisCacheKeyManager(string instanceName)
    {
        InstanceName = instanceName;
    }
    
    public CacheKey GetTrackListensCountKey(int trackId)
        => new(InstanceName, typeof(Track), trackId.ToString(), CacheKeyType.ListensCount);

    public CacheKey GetAlbumListensCountKey(int albumId)
        => new(InstanceName, typeof(Album), albumId.ToString(), CacheKeyType.ListensCount);

    public CacheKey GetUserListensCountKey(int userId)
        => new(InstanceName, typeof(User), userId.ToString(), CacheKeyType.ListensCount);

    
    public CacheKey GetTrackLikesCountKey(int trackId)
        => new(InstanceName, typeof(Track), trackId.ToString(), CacheKeyType.LikesCount);

    public CacheKey GetAlbumLikesCountKey(int albumId)
        => new(InstanceName, typeof(Album), albumId.ToString(), CacheKeyType.LikesCount);

    public CacheKey GetUserLikesCountKey(int userId)
        => new(InstanceName, typeof(User), userId.ToString(), CacheKeyType.LikesCount);


    public CacheKey GetUserFollowersCountKey(int userId)
        => new(InstanceName, typeof(User), userId.ToString(), CacheKeyType.FollowersCount);

    public CacheKey GetUserFollowingsCountKey(int userId)
        => new(InstanceName, typeof(User), userId.ToString(), CacheKeyType.FollowingsCount);
}