using Sevriukoff.Gwalt.Infrastructure.Base;
using Sevriukoff.Gwalt.Infrastructure.Entities;
using Sevriukoff.Gwalt.Infrastructure.Interfaces;

namespace Sevriukoff.Gwalt.Infrastructure.Repositories;

public class LikeRepository : BaseRepository<Like>, ILikeRepository
{
    public LikeRepository(DataDbContext context) : base(context) { }


    public async Task<Like> GetLikeOnTrackAsync(int trackId, int userId)
    {
        var res = Context.Likes.First(x => x.TrackId == trackId && x.LikeById == userId);
        return res;
    }
}