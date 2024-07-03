using Sevriukoff.Gwalt.Application.Models;
using Sevriukoff.Gwalt.Infrastructure.Entities;

namespace Sevriukoff.Gwalt.Application.Interfaces;

public interface ITrackService
{
    Task<IEnumerable<Track>> GetAllAsync();
    Task<IEnumerable<TrackModel>> GetAllByAlbumIdAsync(int albumId, string[]? includes = null, int pageNumber = 1, int pageSize = 10);
    Task<TrackModel?> GetByIdAsync(int id, string[]? includes = null);
    
    Task<int> AddAsync(TrackModel track);
    
    Task<IEnumerable<TrackModel>> GetByUserIdAsync(int userId, string[]? includes, int pageNumber, int pageSize);
}