using Microsoft.EntityFrameworkCore;
using Sevriukoff.Gwalt.Infrastructure.Base;
using Sevriukoff.Gwalt.Infrastructure.Entities;
using Sevriukoff.Gwalt.Infrastructure.Interfaces;

namespace Sevriukoff.Gwalt.Infrastructure.Repositories;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(DataDbContext context) : base(context) { }

    public override async Task<IEnumerable<User>> GetAllAsync(int pageNumber = 1, int pageSize = 10, ISpecification<User>? specification = null)
    {
        var usersBase = await base.GetAllAsync(pageNumber, pageSize, specification);
        
        return usersBase;
    }

    public override async Task<User?> GetByIdAsync(int id, ISpecification<User>? specification = null)
    {
        var userBase = await base.GetByIdAsync(id, specification);
        
        return userBase;
    }

    public async Task<IEnumerable<User>> GetAllWithStaticsAsync(int pageNumber = 1, int pageSize = 10, ISpecification<User>? specification = null)
    {
        var query = Context.Users

            .Include(x => x.Albums)
                .ThenInclude(x => x.Tracks)
                    .ThenInclude(x => x.Genres)

            .Include(x => x.Albums)
                .ThenInclude(x => x.Tracks)
                    .ThenInclude(x => x.Likes)

            .Include(x => x.Albums)
                .ThenInclude(x => x.Tracks)
                    .ThenInclude(x => x.Listens)

            .Include(x => x.Albums)
                .ThenInclude(x => x.Tracks)
                    .ThenInclude(x => x.Shares);
        
        var specQuery = SpecificationEvaluator<User>.GetQuery(query, specification);
        
        var users = await PaginatedList<User>.CreateAsync(specQuery, pageNumber, pageSize);
        
        return users;
    }
    
    public async Task<IEnumerable<User>> GetFollowersAsync(int userId, int pageNumber = 1, int pageSize = 10)
    {
        var followers = await Context.UserFollowers
            .Where(uf => uf.UserId == userId)
            .Include(uf => uf.Follower)
            .Select(uf => uf.Follower)
            .ToListAsync();
        
        return followers;
    }
    
    public async Task<IEnumerable<User>> GetFollowingsAsync(int userId, int pageNumber = 1, int pageSize = 10)
    {
        var followings = await Context.UserFollowers
            .Where(uf => uf.FollowerId == userId)
            .Include(uf => uf.User)
            .Select(uf => uf.User)
            .ToListAsync();
        
        return followings;
    }

    public async Task<IEnumerable<User>> GetAllByAlbumIdAsync(int albumId, int pageNumber = 1, int pageSize = 10, ISpecification<User>? spec = null)
    {
        var users = await GetFilteredAsync(x => x.Albums.Any(y => y.Id == albumId), pageNumber, pageSize, spec);
        
        return users;
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        var user = await Context.Users.FirstOrDefaultAsync(x => x.Email == email);
        
        return user;
    }

    public async Task<IEnumerable<User>> SearchAsync(string query)
    {
        if (query.Length > 3)
        {
            return await Context.Users
                .Where(x => EF.Functions.ToTsVector("russian", x.Name)
                    .Matches(EF.Functions.PlainToTsQuery("russian", query)))
                .ToListAsync();
        }
        
        return await Context.Users
            .Where(x => EF.Functions.ILike(x.Name, $"{query}%"))
            .ToListAsync();
    }

    public async Task<bool> IsUserFollowingAsync(int userId, int followerId)
    {
        return await Context.UserFollowers
            .AnyAsync(x => x.UserId == userId && x.FollowerId == followerId);
    }

    public async Task<UserFollower?> GetFollowAsync(int userId, int followerId)
    {
        return await Context.UserFollowers
            .FirstOrDefaultAsync(x => x.UserId == userId && x.FollowerId == followerId);
    }

    public async Task<int> AddFollowAsync(int userId, int followerId)
    {
        var userFollower = new UserFollower
        {
            UserId = userId,
            FollowerId = followerId,
            CreatedAt = DateTime.UtcNow
        };
        
        await Context.UserFollowers.AddAsync(userFollower);
        await Context.SaveChangesAsync();
        
        return userFollower.UserId;
    }
    
    public async Task<bool> DeleteFollowAsync(int userId, int followerId)
    {
        var userFollower = await Context.UserFollowers
            .FirstOrDefaultAsync(x => x.UserId == userId && x.FollowerId == followerId);
        
        if (userFollower == null)
            return false;
        
        Context.UserFollowers.Remove(userFollower);
        
        return await Context.SaveChangesAsync() > 0;
    }

    public async Task IncrementListensAsync(int userId, int increment)
    {
        await IncrementFieldAsync("ListensCount", userId, increment);
    }

    public async Task IncrementLikesAsync(int userId, int increment)
    {
        await IncrementFieldAsync("LikesCount", userId, increment);
    }
    
    public async Task IncrementFollowersAsync(int userId, int increment)
    {
        await IncrementFieldAsync("FollowersCount", userId, increment);
    }
    
    public async Task IncrementFollowingsAsync(int userId, int increment)
    {
        await IncrementFieldAsync("FollowingCount", userId, increment);
    }
}