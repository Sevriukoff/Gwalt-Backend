using AutoMapper;
using Sevriukoff.Gwalt.Application.Interfaces;
using Sevriukoff.Gwalt.Application.Models;
using Sevriukoff.Gwalt.Application.Specification;
using Sevriukoff.Gwalt.Infrastructure.Entities;
using Sevriukoff.Gwalt.Infrastructure.External;
using Sevriukoff.Gwalt.Infrastructure.Interfaces;

namespace Sevriukoff.Gwalt.Application.Services;

public class TrackService : ITrackService
{
    private readonly ITrackRepository _trackRepository;
    private readonly IFileStorage _fileStorage;
    private readonly IMapper _mapper;
    private readonly AudioProcessor _audioProcessor;

    public TrackService(ITrackRepository trackRepository, IMapper mapper, IFileStorage fileStorage, AudioProcessor audioProcessor)
    {
        _trackRepository = trackRepository;
        _mapper = mapper;
        _fileStorage = fileStorage;
        _audioProcessor = audioProcessor;
    }
    
    public async Task<IEnumerable<Track>> GetAllAsync()
    {
        return await _trackRepository.GetAllAsync();
    }
    
    public async Task<IEnumerable<TrackModel>> GetByUserIdAsync(int userId, string[]? includes, int pageNumber, int pageSize)
    {
        var includeSpec = new IncludingSpecification<Track>(includes);
        var tracksEntity = await _trackRepository.GetAllByUserIdAsync(userId, pageNumber, pageSize, includeSpec);

        return _mapper.Map<IEnumerable<TrackModel>>(tracksEntity);
    }

    public async Task<IEnumerable<TrackModel>> GetAllByAlbumIdAsync(int albumId, string[]? includes = null, int pageNumber = 1, int pageSize = 10)
    {
        var includeSpec = new IncludingSpecification<Track>(includes);
        var tracksEntity = await _trackRepository.GetAllByAlbumIdAsync(albumId, pageNumber, pageSize, includeSpec);

        return _mapper.Map<IEnumerable<TrackModel>>(tracksEntity);
    }

    public async Task<TrackModel?> GetByIdAsync(int id, string[]? includes = null)
    {
        var includeSpec = new IncludingSpecification<Track>(includes);
        var track = await _trackRepository.GetByIdAsync(id, includeSpec);
        
        return track == null ? null : _mapper.Map<TrackModel>(track);
    }
    
    public async Task<int> AddAsync(TrackModel track)
    {
        await using var trackAudio = await _fileStorage.DownloadAsync(track.AudioUrl);
        
        var trackPeaks = _audioProcessor.GetAudioPeaks(trackAudio);
        var trackEntity = _mapper.Map<Track>(track);
        trackEntity.Peaks = new TrackPeaks{ TrackId = track.Id, Peaks = trackPeaks };
        
        var trackId = await _trackRepository.AddAsync(trackEntity);
        
        return trackId;
    }
}