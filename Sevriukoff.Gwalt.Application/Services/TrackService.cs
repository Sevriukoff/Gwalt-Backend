using Sevriukoff.Gwalt.Application.Interfaces;
using Sevriukoff.Gwalt.Infrastructure.Entities;
using Sevriukoff.Gwalt.Infrastructure.Interfaces;

namespace Sevriukoff.Gwalt.Application.Services;

public class TrackService : ITrackService
{
    private readonly ITrackRepository _trackRepository;
    
    public TrackService(ITrackRepository trackRepository)
    {
        _trackRepository = trackRepository;
    }
    
    public async Task<IEnumerable<Track>> GetAllAsync()
    {
        return await _trackRepository.GetAllAsync();
    }

    public async Task<Track?> GetByIdAsync(int id)
    {
        return await _trackRepository.GetByIdAsync(id);
    }
}