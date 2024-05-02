using Sevriukoff.Gwalt.Infrastructure.Base;
using Sevriukoff.Gwalt.Infrastructure.Entities;

namespace Sevriukoff.Gwalt.Infrastructure.Repositories;

public class AlbumRepository : BaseRepository<Album>
{
    public AlbumRepository(DataDbContext context) : base(context)
    {
        
    }
}