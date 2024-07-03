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

    public async Task<IEnumerable<Track>> GetAllByAlbumIdAsync(int albumId, int pageNumber, int pageSize, ISpecification<Track>? spec = null)
    {
        var query = Context.Tracks
            .Where(x => x.AlbumId == albumId)
            .Include(x => x.Album)
                .ThenInclude(x => x.Authors)
            .Include(x => x.Genres)
            .AsQueryable();
        
        var tracks = await GetFilteredAsync(query, pageNumber, pageSize, spec);

        return tracks;
    }

    public async Task<IEnumerable<Track>> GetAllByUserIdAsync(int userId, int pageNumber, int pageSize, ISpecification<Track>? spec = null)
    {
        var tracks = await GetFilteredAsync(x => x.Album.Authors.Any(x => x.Id == userId), pageNumber, pageSize, spec);

        return tracks;
    }

    public async Task<(int, int[])> GetAuthorsIdsByTrackIdAsync(int trackId)
    {
        var result = await Context.Tracks
            .Where(t => t.Id == trackId && t.AlbumId != null)
            .Select(t => new 
            {
                AlbumId = t.AlbumId,
                AuthorIds = Context.Users
                    .Where(a => a.Albums.Any(al => al.Id == t.AlbumId))
                    .Select(a => a.Id)
                    .Distinct()
                    .ToArray()
            })
            .FirstOrDefaultAsync();

        return (result.AlbumId, result.AuthorIds);
    }
    
    public async Task IncrementListensAsync(int trackId, int increment)
    {
        await IncrementFieldAsync("ListensCount", trackId, increment);
    }

    public async Task IncrementLikesAsync(int trackId, int increment)
    {
        await IncrementFieldAsync("LikesCount", trackId, increment);
    }
}