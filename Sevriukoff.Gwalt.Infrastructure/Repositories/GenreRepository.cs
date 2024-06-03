using Sevriukoff.Gwalt.Infrastructure.Base;
using Sevriukoff.Gwalt.Infrastructure.Entities;
using Sevriukoff.Gwalt.Infrastructure.Interfaces;

namespace Sevriukoff.Gwalt.Infrastructure.Repositories;

public class GenreRepository : BaseRepository<Genre>, IGenreRepository
{
    public GenreRepository(DataDbContext context) : base(context) { }
}