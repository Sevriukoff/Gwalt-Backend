using Sevriukoff.Gwalt.Application.Models;
using Sevriukoff.Gwalt.Infrastructure.Entities;

namespace Sevriukoff.Gwalt.Application.Interfaces;

public interface ITrackService
{
    Task<IEnumerable<Track>> GetAllAsync();
    Task<TrackModel?> GetByIdAsync(int id, string[]? includes = null);
    
    Task<int> AddAsync(TrackModel track);
}