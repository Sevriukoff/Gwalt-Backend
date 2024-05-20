using Sevriukoff.Gwalt.Infrastructure.Base;
using Sevriukoff.Gwalt.Infrastructure.Entities;
using Sevriukoff.Gwalt.Infrastructure.Interfaces;

namespace Sevriukoff.Gwalt.Infrastructure.Repositories;

public class AlbumRepository : BaseRepository<Album>, IAlbumRepository
{
    public AlbumRepository(DataDbContext context) : base(context)
    {
    }
}