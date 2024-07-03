using AutoMapper;
using Sevriukoff.Gwalt.Application.Helpers;
using Sevriukoff.Gwalt.Application.Interfaces;
using Sevriukoff.Gwalt.Application.Models;
using Sevriukoff.Gwalt.Application.Specification;
using Sevriukoff.Gwalt.Infrastructure.Entities;
using Sevriukoff.Gwalt.Infrastructure.Interfaces;

namespace Sevriukoff.Gwalt.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly PasswordHasher _passwordHasher;
    private readonly IMapper _autoMapper;
    
    public UserService(IUserRepository userRepository, PasswordHasher passwordHasher, IMapper mapper)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _autoMapper = mapper;
    }
    public async Task<IEnumerable<UserModel>> GetAllAsync(string[]? includes, string? orderBy)
    {
        var includeSpec = new IncludingSpecification<User>(includes);
        var orderSpec = new SortingSpecification<User>(orderBy);
        var compositeSpec = includeSpec.And(orderSpec);
        
        var userModels = await _userRepository.GetAllAsync(compositeSpec);
        
        return userModels.Select(x => _autoMapper.Map<UserModel>(x));
    }
    
    public async Task<UserModel> GetByIdAsync(int id, string[]? includes)
    {
        var includeSpec = new IncludingSpecification<User>(includes);
        
        var userEntity = await _userRepository.GetByIdAsync(id, includeSpec);
        var userModel = _autoMapper.Map<UserModel>(userEntity);
        
        return userModel;
    }

    public async Task<IEnumerable<UserModel>> GetAllWithStaticsAsync()
    {
        var userEntities = await _userRepository.GetAllWithStaticsAsync();
        var userModels = userEntities.Select(x => _autoMapper.Map<UserModel>(x));

        return userModels;
    }

    public async Task<int> AddAsync(string name, string email, string password)
    {
        var salt = _passwordHasher.GenerateSalt();
        var passwordHash = _passwordHasher.HashPassword(password, salt);
        
        var userEntity = new User
        {
            Name = name,
            Email = email,
            PasswordHash = passwordHash,
            PasswordSalt = salt,
            RegistrationDate = DateTime.UtcNow,
            AvatarUrl = "https://storage.yandexcloud.net/id-gwalt-storage/image/AvatarDefault.jpeg",
            BackgroundUrl = "https://storage.yandexcloud.net/id-gwalt-storage/image/BackgroundDefault.jpeg"
        };

        var id = await _userRepository.AddAsync(userEntity);

        return id;
    }

    public async Task<UserModel> UpdateAsync(UserModel user)
    {
        throw new NotImplementedException();
    }

    public async Task<int> FollowAsync(int userId, int followerId)
    {
        if (userId == followerId)
            return 0;
        
        var followIsExist = await _userRepository.IsUserFollowingAsync(userId, followerId);

        if (followIsExist)
            return 0;

        var id = await _userRepository.AddFollowAsync(userId, followerId);
        
        await _followerCacheClient.IncrementFollowersAsync(userId, followerId);
        
        return id;
    }
    
    public async Task<bool> UnfollowAsync(int userId, int followerId)
    {
        if (userId == followerId)
            return false;
        
        var followIsExist = await _userRepository.IsUserFollowingAsync(userId, followerId);

        if (!followIsExist)
            return false;

        var result = await _userRepository.DeleteFollowAsync(userId, followerId);
        
        await _followerCacheClient.DecrementFollowersAsync(userId, followerId);
        
        return result;
    }

    public async Task<DateTime> GetFollowAsync(int userId, int followerId)
    {
        var userFollower = await _userRepository.GetFollowAsync(userId, followerId);
        
        if (userFollower == null)
            return DateTime.MinValue;
        
        return userFollower.CreatedAt;
    }

    public int GetTotalMetricsByTracks(UserModel user, Func<TrackModel, int> metricSelector)
    {
        var totalMetric = user.Albums.Sum(x => x.Tracks.Sum(metricSelector));
        
        return totalMetric;
    }
    
    public int GetTotalMetricsByAlbums(UserModel user, Func<AlbumModel, int> metricSelector)
    {
        var totalMetric = user.Albums.Sum(metricSelector);
        
        return totalMetric;
    }

    public int GetTracksCount(UserModel? user)
    {
        return user.Albums.Sum(x => x.Tracks.Count);
    }

    public string[] GetAllGenres(UserModel user)
    {
        return user.Albums.SelectMany(album => album.Tracks.SelectMany(track => track.Genres))
            .Select(x => x.Name)
            .Distinct()
            .ToArray();
    }
}