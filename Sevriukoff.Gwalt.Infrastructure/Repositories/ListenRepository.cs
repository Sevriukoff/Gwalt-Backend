using Sevriukoff.Gwalt.Infrastructure.Base;
using Sevriukoff.Gwalt.Infrastructure.Entities;
using Sevriukoff.Gwalt.Infrastructure.Interfaces;

namespace Sevriukoff.Gwalt.Infrastructure.Repositories;

public class ListenRepository : BaseRepository<Listen>, IListenRepository
{
    public ListenRepository(DataDbContext context) : base(context) { }
}