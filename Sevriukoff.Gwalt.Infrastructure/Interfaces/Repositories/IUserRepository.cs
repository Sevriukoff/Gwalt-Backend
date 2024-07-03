using Sevriukoff.Gwalt.Infrastructure.Base;
using Sevriukoff.Gwalt.Infrastructure.Entities;

namespace Sevriukoff.Gwalt.Infrastructure.Interfaces;

public interface IUserRepository : IRepository<User>
{
    Task<IEnumerable<User>> GetAllWithStaticsAsync(int pageNumber = 1, int pageSize = 10, ISpecification<User>? specification = null);
    Task<IEnumerable<User>> GetFollowersAsync(int userId, int pageNumber = 1, int pageSize = 10);
    Task<IEnumerable<User>> GetFollowingsAsync(int userId, int pageNumber = 1, int pageSize = 10);
    Task<IEnumerable<User>> GetAllByAlbumIdAsync(int albumId, int pageNumber = 1, int pageSize = 10, ISpecification<User>? spec = null);
    Task<User?> GetByEmailAsync(string email);
    Task<IEnumerable<User>> SearchAsync(string query);
    
    Task<bool> IsUserFollowingAsync(int userId, int followerId);
    Task<UserFollower?> GetFollowAsync (int userId, int followerId);
    Task<int> AddFollowAsync(int userId, int followerId);
    Task<bool> DeleteFollowAsync(int userId, int followerId);

    Task IncrementListensAsync(int userId, int increment);
    Task IncrementLikesAsync(int trackId, int increment);
    Task IncrementFollowersAsync(int userId, int increment);
    Task IncrementFollowingsAsync(int userId, int increment);
}