using Sevriukoff.Gwalt.Infrastructure.Base;
using Sevriukoff.Gwalt.Infrastructure.Interfaces;

namespace Sevriukoff.Gwalt.Infrastructure.Caching;

public class BaseCacheApplier<T> : ICacheApplier<T> where T : BaseEntity, IHasListens, IHasLikes
{
    private readonly LikeCacheClient _likeCacheClient;
    private readonly ListenCacheClient _listenCacheClient;

    public BaseCacheApplier(LikeCacheClient likeCacheClient, ListenCacheClient listensCacheClient)
    {
        _likeCacheClient = likeCacheClient;
        _listenCacheClient = listensCacheClient;
    }

    public virtual async Task ApplyCacheAsync(T? entity)
    {
        if (entity is null)
            return;
        
        var likesCountInCache = await _likeCacheClient.GetLikesCountByUserAsync(entity.Id);
        var listensCountInCache = await _listenCacheClient.GetListensCountByUserAsync(entity.Id);

        entity.LikesCount += likesCountInCache;
        entity.ListensCount += listensCountInCache;
    }

    public virtual async Task ApplyCacheAsync(IEnumerable<T>? entities)
    {
        if (entities is null)
            return;
        
        foreach (var entity in entities)
            await ApplyCacheAsync(entity);
    }
}