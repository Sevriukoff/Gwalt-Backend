using Sevriukoff.Gwalt.Infrastructure.Base;
using Sevriukoff.Gwalt.Infrastructure.Caching;
using Sevriukoff.Gwalt.Infrastructure.Entities;
using Sevriukoff.Gwalt.Infrastructure.Interfaces;

namespace Sevriukoff.Gwalt.Infrastructure.Repositories;

public class ListenRepository : BaseRepository<Listen>, IListenRepository
{
    private readonly LikeCacheClient _likeCacheClient;
    private readonly ListenCacheClient _listenCacheClient;
    
    public ListenRepository(DataDbContext context, LikeCacheClient likeCacheClient, ListenCacheClient listenCacheClient) : base(context)
    {
        _likeCacheClient = likeCacheClient;
        _listenCacheClient = listenCacheClient;
    }

    public override async Task<IEnumerable<Listen>> GetAllAsync(int pageNumber = 1, int pageSize = 10, ISpecification<Listen>? specification = null)
    {
        var listensBase = (await base.GetAllAsync(pageNumber, pageSize, specification)).ToList();
        
        await Task.WhenAll(listensBase.Select(x => ApplyCacheAsync(x.Track)));
        
        return listensBase;
    }
    
    private async Task ApplyCacheAsync(Track? track)
    {
        if (track == null)
            return;
        
        var likesCountInCache = await _likeCacheClient.GetLikesCountByTrackAsync(track.Id);
        var listensCountInCache = await _listenCacheClient.GetListensCountByTrackAsync(track.Id);
        
        track.LikesCount += likesCountInCache;
        track.ListensCount += listensCountInCache;
    }
}