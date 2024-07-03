using Sevriukoff.Gwalt.Infrastructure.Entities;

namespace Sevriukoff.Gwalt.Infrastructure.Caching;

public class UserCacheApplier : BaseCacheApplier<User>
{
    private readonly FollowerCacheClient _followerCacheClient;

    public UserCacheApplier(LikeCacheClient likeCacheClient, ListenCacheClient listenCacheClient, FollowerCacheClient followerCacheClient)
        : base(likeCacheClient, listenCacheClient)
    {
        _followerCacheClient = followerCacheClient;
    }

    public override async Task ApplyCacheAsync(User? user)
    {
        if (user is null)
            return;
        
        await base.ApplyCacheAsync(user);

        var followersCountInCache = await _followerCacheClient.GetFollowersCountByUserAsync(user.Id);
        var followingsCountInCache = await _followerCacheClient.GetFollowingsCountByUserAsync(user.Id);

        user.FollowersCount += followersCountInCache;
        user.FollowingCount += followingsCountInCache;
    }

    public override async Task ApplyCacheAsync(IEnumerable<User>? users)
    {
        if (users is null)
            return;
        
        foreach (var user in users)
            await ApplyCacheAsync(user);
    }
}