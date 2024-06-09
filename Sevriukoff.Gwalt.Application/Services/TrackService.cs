using AutoMapper;
using Sevriukoff.Gwalt.Application.Helpers;
using Sevriukoff.Gwalt.Application.Interfaces;
using Sevriukoff.Gwalt.Application.Models;
using Sevriukoff.Gwalt.Application.Specification;
using Sevriukoff.Gwalt.Infrastructure.Entities;
using Sevriukoff.Gwalt.Infrastructure.Interfaces;

namespace Sevriukoff.Gwalt.Application.Services;

public class TrackService : ITrackService
{
    private readonly ITrackRepository _trackRepository;
    private readonly IMapper _mapper;
    private readonly IAlbumRepository _albumRepository;
    private readonly IListenCacheService _listenCacheService;

    public TrackService(ITrackRepository trackRepository, IMapper mapper, IAlbumRepository albumRepository,
        IListenCacheService listenCacheService)
    {
        _trackRepository = trackRepository;
        _mapper = mapper;
        _albumRepository = albumRepository;
        _listenCacheService = listenCacheService;
    }
    
    public async Task<IEnumerable<Track>> GetAllAsync()
    {
        return await _trackRepository.GetAllAsync();
    }

    public async Task<TrackModel?> GetByIdAsync(int id, string[]? includes = null)
    {
        var includeSpec = new IncludingSpecification<Track>(includes);
        var track = await _trackRepository.GetByIdAsync(id, includeSpec);
        
        return track == null ? null : _mapper.Map<TrackModel>(track);
    }
    
    public async Task<int> AddAsync(TrackModel track)
    {
        var trackEntity = _mapper.Map<Track>(track);
        var trackId = await _trackRepository.AddAsync(trackEntity);
        
        return trackId;
    }
    
    public async Task UpdateDatabaseFromCacheAsync()
    {
        var trackPlayCounts = await _listenCacheService.GetTrackPlayCountsAsync();
        var albumPlayCounts = await _listenCacheService.GetAlbumPlayCountsAsync();

        foreach (var trackPlayCount in trackPlayCounts)
        {
            var track = await _trackRepository.GetByIdAsync(trackPlayCount.Key);
            if (track != null)
            {
                track.PlayCount += trackPlayCount.Value;
                await _trackRepository.UpdateAsync(track);
            }
        }

        foreach (var albumPlayCount in albumPlayCounts)
        {
            var album = await _albumRepository.GetByIdAsync(albumPlayCount.Key);
            if (album != null)
            {
                album.PlayCount += albumPlayCount.Value;
                await _albumRepository.UpdateAsync(album);
            }
        }

        await _listenCacheService.ClearTrackPlayCountsAsync();
        await _listenCacheService.ClearAlbumPlayCountsAsync();
    }
}