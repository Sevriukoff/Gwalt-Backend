using Sevriukoff.Gwalt.Infrastructure.Base;
using Sevriukoff.Gwalt.Infrastructure.Entities;
using Sevriukoff.Gwalt.Infrastructure.Interfaces;

namespace Sevriukoff.Gwalt.Infrastructure.Repositories;

public class LikeRepository : BaseRepository<Like>, ILikeRepository
{
    public LikeRepository(DataDbContext context) : base(context) { }
}