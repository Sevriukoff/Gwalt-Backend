using Sevriukoff.Gwalt.Infrastructure.Base;
using Sevriukoff.Gwalt.Infrastructure.Entities;
using Sevriukoff.Gwalt.Infrastructure.Interfaces;

namespace Sevriukoff.Gwalt.Infrastructure.Repositories;

public class PlaylistRepository : BaseRepository<Playlist>, IPlaylistRepository
{
    public PlaylistRepository(DataDbContext context) : base(context) { }
}