using Sevriukoff.Gwalt.Infrastructure.Base;
using Sevriukoff.Gwalt.Infrastructure.Caching;
using Sevriukoff.Gwalt.Infrastructure.Entities;
using Sevriukoff.Gwalt.Infrastructure.Interfaces;

namespace Sevriukoff.Gwalt.Infrastructure.Repositories;

public class AlbumRepositoryWithCache : BaseRepositoryWithCache<Album>, IAlbumRepository
{
    private readonly IAlbumRepository _albumRepository;
    
    public AlbumRepositoryWithCache(IRepository<Album> repository, ICacheApplier<Album> cacheApplier,
        IAlbumRepository albumRepository) : base(repository, cacheApplier)
    {
        _albumRepository = albumRepository;
    }

    public async Task<IEnumerable<Album>> SearchAsync(string query)
    {
        var albums = (await _albumRepository.SearchAsync(query)).ToList();
        await CacheApplier.ApplyCacheAsync(albums);
        return albums;
    }

    public async Task<int[]> GetAuthorsIds(int albumId)
        => await _albumRepository.GetAuthorsIds(albumId);

    public async Task IncrementListensAsync(int albumId, int increment)
    {
        await _albumRepository.IncrementListensAsync(albumId, increment);
    }

    public async Task IncrementLikesAsync(int trackId, int increment)
    {
        await _albumRepository.IncrementLikesAsync(trackId, increment);
    }
}