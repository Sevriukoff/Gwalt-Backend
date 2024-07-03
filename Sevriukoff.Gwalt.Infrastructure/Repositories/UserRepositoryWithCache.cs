using Sevriukoff.Gwalt.Infrastructure.Base;
using Sevriukoff.Gwalt.Infrastructure.Caching;
using Sevriukoff.Gwalt.Infrastructure.Entities;
using Sevriukoff.Gwalt.Infrastructure.Interfaces;

namespace Sevriukoff.Gwalt.Infrastructure.Repositories;

public class UserRepositoryWithCache: BaseRepositoryWithCache<User>, IUserRepository
{
    private readonly IUserRepository _userRepository;
    
    public UserRepositoryWithCache(IRepository<User> repository, ICacheApplier<User> cacheApplier,
        IUserRepository userRepository) : base(repository, cacheApplier)
    {
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<User>> GetAllWithStaticsAsync(int pageNumber = 1, int pageSize = 10, ISpecification<User>? specification = null)
    {
        var users = (await _userRepository.GetAllWithStaticsAsync(pageNumber, pageSize, specification)).ToList();
        await CacheApplier.ApplyCacheAsync(users);
        return users;
    }

    public async Task<IEnumerable<User>> GetFollowersAsync(int userId, int pageNumber = 1, int pageSize = 10)
    {
        var users = (await _userRepository.GetFollowersAsync(userId, pageNumber, pageSize)).ToList();
        await CacheApplier.ApplyCacheAsync(users);
        return users;
    }

    public async Task<IEnumerable<User>> GetFollowingsAsync(int userId, int pageNumber = 1, int pageSize = 10)
    {
        var users = (await _userRepository.GetFollowingsAsync(userId, pageNumber, pageSize)).ToList();
        await CacheApplier.ApplyCacheAsync(users);
        return users;
    }

    public async Task<IEnumerable<User>> GetAllByAlbumIdAsync(int albumId, int pageNumber = 1, int pageSize = 10, ISpecification<User>? spec = null)
    {
        var users = (await _userRepository.GetAllByAlbumIdAsync(albumId, pageNumber, pageSize, spec)).ToList();
        await CacheApplier.ApplyCacheAsync(users);
        return users;
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        var user = await _userRepository.GetByEmailAsync(email);
        await CacheApplier.ApplyCacheAsync(user);
        return user;
    }

    public async Task<IEnumerable<User>> SearchAsync(string query)
    {
        var users = (await _userRepository.SearchAsync(query)).ToList();
        await CacheApplier.ApplyCacheAsync(users);
        return users;
    }

    public async Task<bool> IsUserFollowingAsync(int userId, int followerId)
        => await _userRepository.IsUserFollowingAsync(userId, followerId);

    public async Task<UserFollower?> GetFollowAsync(int userId, int followerId)
    {
        var userFollow = await _userRepository.GetFollowAsync(userId, followerId);
        
        if (userFollow == null)
            return userFollow;
        
        await CacheApplier.ApplyCacheAsync(userFollow.Follower);
        
        await CacheApplier.ApplyCacheAsync(userFollow.User);

        return userFollow;
    }

    public async Task<int> AddFollowAsync(int userId, int followerId)
    {
        return await _userRepository.AddFollowAsync(userId, followerId);
    }

    public async Task<bool> DeleteFollowAsync(int userId, int followerId)
        => await _userRepository.DeleteFollowAsync(userId, followerId);

    public async Task IncrementListensAsync(int userId, int increment)
    {
        await _userRepository.IncrementListensAsync(userId, increment);
    }

    public async Task IncrementLikesAsync(int trackId, int increment)
    {
        await _userRepository.IncrementLikesAsync(trackId, increment);
    }

    public async Task IncrementFollowersAsync(int userId, int increment)
    {
        await _userRepository.IncrementFollowersAsync(userId, increment);
    }

    public async Task IncrementFollowingsAsync(int userId, int increment)
    {
        await _userRepository.IncrementFollowingsAsync(userId, increment);
    }
}