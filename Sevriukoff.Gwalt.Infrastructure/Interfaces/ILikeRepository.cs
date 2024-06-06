using Sevriukoff.Gwalt.Infrastructure.Entities;

namespace Sevriukoff.Gwalt.Infrastructure.Interfaces;

public interface ILikeRepository : IRepository<Like>
{
    Task<Like> GetLikeOnTrackAsync(int trackId, int userId);
}