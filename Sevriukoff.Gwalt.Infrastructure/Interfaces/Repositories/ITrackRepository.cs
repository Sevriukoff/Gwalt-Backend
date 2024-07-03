using Sevriukoff.Gwalt.Infrastructure.Base;
using Sevriukoff.Gwalt.Infrastructure.Entities;

namespace Sevriukoff.Gwalt.Infrastructure.Interfaces;

public interface ITrackRepository : IRepository<Track>
{
    Task<IEnumerable<Track>> SearchAsync(string query);
    Task<IEnumerable<Track>> GetAllByAlbumIdAsync(int albumId, int pageNumber = 1, int pageSize = 10, ISpecification<Track>? spec = null);
    Task<IEnumerable<Track>> GetAllByUserIdAsync(int userId, int pageNumber = 1, int pageSize = 10, ISpecification<Track>? spec = null);
    
    Task<(int, int[])> GetAuthorsIdsByTrackIdAsync(int trackId);
    Task IncrementListensAsync(int trackId, int increment);
    Task IncrementLikesAsync(int trackId, int increment);
}