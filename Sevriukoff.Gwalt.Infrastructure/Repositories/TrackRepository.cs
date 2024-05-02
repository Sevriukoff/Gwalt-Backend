using Sevriukoff.Gwalt.Infrastructure.Base;
using Sevriukoff.Gwalt.Infrastructure.Entities;
using Sevriukoff.Gwalt.Infrastructure.Interfaces;

namespace Sevriukoff.Gwalt.Infrastructure.Repositories;

public class TrackRepository : BaseRepository<Track>, ITrackRepository
{
    public TrackRepository(DataDbContext context) : base(context)
    {
        
    }
}