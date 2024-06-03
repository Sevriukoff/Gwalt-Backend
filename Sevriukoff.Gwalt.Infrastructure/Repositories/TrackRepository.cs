using Microsoft.EntityFrameworkCore;
using Sevriukoff.Gwalt.Infrastructure.Base;
using Sevriukoff.Gwalt.Infrastructure.Entities;
using Sevriukoff.Gwalt.Infrastructure.Interfaces;

namespace Sevriukoff.Gwalt.Infrastructure.Repositories;

public class TrackRepository : BaseRepository<Track>, ITrackRepository
{
    public TrackRepository(DataDbContext context) : base(context) { }

    public override async Task<int> AddAsync(Track entity)
    {
        foreach (var genre in entity.Genres)
        {
            Context.Entry(genre).State = EntityState.Unchanged;
        }
        
        return await base.AddAsync(entity);
    }
}