using Microsoft.EntityFrameworkCore;
using Sevriukoff.Gwalt.Infrastructure.Base;
using Sevriukoff.Gwalt.Infrastructure.Entities;
using Sevriukoff.Gwalt.Infrastructure.Interfaces;

namespace Sevriukoff.Gwalt.Infrastructure.Repositories;

public class AlbumRepository : BaseRepository<Album>, IAlbumRepository
{
    public AlbumRepository(DataDbContext context) : base(context) { }

    public override Task<int> AddAsync(Album entity)
    {
        Context.Entry(entity.Genre).State = EntityState.Unchanged;
        
        foreach (var author in entity.Authors)
            Context.Entry(author).State = EntityState.Unchanged;
        
        return base.AddAsync(entity);
    }

    public override async Task<IEnumerable<Album>> GetAllAsync(int pageNumber = 1, int pageSize = 10, ISpecification<Album>? specification = null)
    {
        var albums = await base.GetAllAsync(pageNumber, pageSize, specification);
        
        return albums;
    }
    
    public override async Task<Album?> GetByIdAsync(int id, ISpecification<Album>? specification = null)
    {
        var album = await base.GetByIdAsync(id, specification);
        
        return album;
    }

    public async Task<IEnumerable<Album>> SearchAsync(string query)
    {
        if (query.Length > 3)
        {
            return await Context.Albums
                .Where(x => EF.Functions.ToTsVector("russian", x.Title)
                    .Matches(EF.Functions.PlainToTsQuery("russian", query)))
                .Include(x => x.Authors)
                .ToListAsync();
        }
        
        return await Context.Albums
            .Where(x => EF.Functions.ILike(x.Title, $"{query}%"))
            .Include(x => x.Authors)
            .ToListAsync();
    }

    }
}