using Microsoft.EntityFrameworkCore;
using Sevriukoff.Gwalt.Infrastructure.Base;
using Sevriukoff.Gwalt.Infrastructure.Entities;
using Sevriukoff.Gwalt.Infrastructure.Interfaces;

namespace Sevriukoff.Gwalt.Infrastructure.Repositories;

public class TrackRepository : BaseRepository<Track>, ITrackRepository
{
    public TrackRepository(DataDbContext context) : base(context) { }

    public override async Task<IEnumerable<Track>> GetAllAsync(int pageNumber = 1, int pageSize = 10, ISpecification<Track>? specification = null)
    {
        var tracksBase = await base.GetAllAsync(pageNumber, pageSize, specification);
    
        return tracksBase;
    }

    public override async Task<Track?> GetByIdAsync(int id, ISpecification<Track>? specification = null)
    {
        var trackBase = await base.GetByIdAsync(id, specification);

        return trackBase;
    }

    public override async Task<int> AddAsync(Track entity)
    {
        foreach (var genre in entity.Genres)
            Context.Entry(genre).State = EntityState.Unchanged;
        
        return await base.AddAsync(entity);
    }
    
    public async Task<IEnumerable<Track>> SearchAsync(string query)
    {
        if (query.Length > 3)
        {
            return await Context.Tracks
                .Where(x => EF.Functions.ToTsVector("russian", x.Title)
                    .Matches(EF.Functions.PlainToTsQuery("russian", query)))
                .Include(x => x.Album)
                    .ThenInclude(x => x.Authors)
                .ToListAsync();
        }
        
        return await Context.Tracks
            .Where(x => EF.Functions.ILike(x.Title, $"{query}%"))
            .Include(x => x.Album)
                .ThenInclude(x => x.Authors)
            .ToListAsync();
    }

}