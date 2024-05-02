using Microsoft.EntityFrameworkCore;
using Sevriukoff.Gwalt.Infrastructure.Base;
using Sevriukoff.Gwalt.Infrastructure.Entities;
using Sevriukoff.Gwalt.Infrastructure.Interfaces;

namespace Sevriukoff.Gwalt.Infrastructure.Repositories;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(DataDbContext context) : base(context)
    {
        
    }

    public async Task<IEnumerable<User>> GetAllWithStaticsAsync()
    {
        var users = await Context.Users
            .Include(x => x.Albums)
                .ThenInclude(x => x.Tracks)
                    .ThenInclude(x => x.Genres)
            .ToListAsync();
        
        return users;
    }
}