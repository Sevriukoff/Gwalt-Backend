using Sevriukoff.Gwalt.Infrastructure.Base;
using Sevriukoff.Gwalt.Infrastructure.Entities;

namespace Sevriukoff.Gwalt.Infrastructure.Interfaces;

public interface IAlbumRepository : IRepository<Album>
{
    Task<IEnumerable<Album>> SearchAsync(string query);
    Task<int[]> GetAuthorsIds(int albumId);
    Task IncrementListensAsync(int albumId, int increment);
    Task IncrementLikesAsync(int trackId, int increment);
}