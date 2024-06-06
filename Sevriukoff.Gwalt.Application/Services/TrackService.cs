using AutoMapper;
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

    public TrackService(ITrackRepository trackRepository, IMapper mapper)
    {
        _trackRepository = trackRepository;
        _mapper = mapper;
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
}