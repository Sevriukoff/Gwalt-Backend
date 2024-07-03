using Sevriukoff.Gwalt.Infrastructure.Base;
using Sevriukoff.Gwalt.Infrastructure.Caching;
using Sevriukoff.Gwalt.Infrastructure.Entities;
using Sevriukoff.Gwalt.Infrastructure.Interfaces;

namespace Sevriukoff.Gwalt.Infrastructure.Repositories;

public class TrackRepositoryWithCache : BaseRepositoryWithCache<Track>, ITrackRepository
{
    private readonly ITrackRepository _trackRepository;
    
    public TrackRepositoryWithCache(IRepository<Track> repository, ICacheApplier<Track> cacheApplier,
        ITrackRepository trackRepository) : base(repository, cacheApplier)
    {
        _trackRepository = trackRepository;
    }

    public async Task<IEnumerable<Track>> SearchAsync(string query)
    {
        var tracks = (await _trackRepository.SearchAsync(query)).ToList();
        await CacheApplier.ApplyCacheAsync(tracks);
        return tracks;
    }

    public async Task<IEnumerable<Track>> GetAllByAlbumIdAsync(int albumId, int pageNumber = 1, int pageSize = 10,
        ISpecification<Track>? spec = null)
    {
        var tracks = (await _trackRepository.GetAllByAlbumIdAsync(albumId, pageNumber, pageSize, spec)).ToList();
        await CacheApplier.ApplyCacheAsync(tracks);
        return tracks;
    }

    public async Task<IEnumerable<Track>> GetAllByUserIdAsync(int userId, int pageNumber = 1, int pageSize = 10,
        ISpecification<Track>? spec = null)
    {
        var tracks = (await _trackRepository.GetAllByUserIdAsync(userId, pageNumber, pageSize, spec)).ToList();
        await CacheApplier.ApplyCacheAsync(tracks);
        return tracks;
    }

    public async Task<(int, int[])> GetAuthorsIdsByTrackIdAsync(int trackId)
        => await _trackRepository.GetAuthorsIdsByTrackIdAsync(trackId);

    public async Task IncrementListensAsync(int trackId, int increment)
    {
        await _trackRepository.IncrementListensAsync(trackId, increment);
    }

    public async Task IncrementLikesAsync(int trackId, int increment)
    {
        await _trackRepository.IncrementLikesAsync(trackId, increment);
    }
}