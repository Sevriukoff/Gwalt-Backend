using AutoMapper;
using Sevriukoff.Gwalt.Application.Helpers;
using Sevriukoff.Gwalt.Application.Interfaces;
using Sevriukoff.Gwalt.Application.Models;
using Sevriukoff.Gwalt.Application.Specification;
using Sevriukoff.Gwalt.Infrastructure.Caching;
using Sevriukoff.Gwalt.Infrastructure.Entities;
using Sevriukoff.Gwalt.Infrastructure.External;
using Sevriukoff.Gwalt.Infrastructure.Interfaces;

namespace Sevriukoff.Gwalt.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _autoMapper;
    private readonly PasswordHasher _passwordHasher;
    private readonly FollowerCacheClient _followerCacheClient;
    private readonly ImageProcessor _imageProcessor;
    private readonly IFileService _fileService;
    
    public UserService(IUserRepository userRepository,
        PasswordHasher passwordHasher,
        FollowerCacheClient followerCacheClient,
        IMapper mapper,
        ImageProcessor imageProcessor, IFileService fileService)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _autoMapper = mapper;
        _imageProcessor = imageProcessor;
        _fileService = fileService;
        _followerCacheClient = followerCacheClient;
    }
    public async Task<IEnumerable<UserModel>> GetAllAsync(string[]? includes, string? orderBy, int pageNumber = 1, int pageSize = 10)
    {
        var includeSpec = new IncludingSpecification<User>(includes);
        var orderSpec = new SortingSpecification<User>(orderBy);
        var compositeSpec = includeSpec.And(orderSpec);
        
        var userModels = await _userRepository.GetAllAsync(pageNumber, pageSize, compositeSpec);
        
        return userModels.Select(x => _autoMapper.Map<UserModel>(x));
    }
    
    public async Task<IEnumerable<UserModel?>> GetAllWithStaticsAsync(string? orderBy, int pageNumber = 1, int pageSize = 10)
    {
        var orderSpec = new SortingSpecification<User>(orderBy);
        
        var userEntities = await _userRepository.GetAllWithStaticsAsync(pageNumber, pageSize, orderSpec);
        IEnumerable<UserModel?> userModels = userEntities.Select(x => _autoMapper.Map<UserModel>(x));

        return userModels;
    }

    public async Task<IEnumerable<UserModel>> GetAllByAlbumIdAsync(int albumId, string[]? includes = null, int pageNumber = 1, int pageSize = 10)
    {
        var includeSpec = new IncludingSpecification<User>(includes);
        
        var userEntities = await _userRepository.GetAllByAlbumIdAsync(albumId, pageNumber, pageSize, includeSpec);
        
        return _autoMapper.Map<IEnumerable<UserModel>>(userEntities);
    }

    public async Task<UserModel?> GetByIdAsync(int id, string[]? includes)
    {
        var includeSpec = new IncludingSpecification<User>(includes);
        
        var userEntity = await _userRepository.GetByIdAsync(id, includeSpec);
        var userModel = _autoMapper.Map<UserModel>(userEntity);
        
        return userModel;
    }
    
    public async Task<IEnumerable<UserModel>> GetFollowersAsync(int userId, int pageNumber = 1, int pageSize = 10)
    {
        var followers = await _userRepository.GetFollowersAsync(userId, pageNumber, pageSize);
        
        return _autoMapper.Map<IEnumerable<UserModel>>(followers);
    }
    
    public async Task<IEnumerable<UserModel>> GetFollowingsAsync(int userId, int pageNumber = 1, int pageSize = 10)
    {
        var followings = await _userRepository.GetFollowingsAsync(userId, pageNumber, pageSize);

        var userModels = followings.Select(_autoMapper.Map<UserModel>);
        
        return userModels;
    }

    public async Task<int> AddAsync(UserModel userModel, string password, bool isDefaultImages)
    {
        var salt = _passwordHasher.GenerateSalt();
        var passwordHash = _passwordHasher.HashPassword(password, salt);

        userModel.AvatarUrl = "https://storage.yandexcloud.net/id-gwalt-storage/image/AvatarDefault.jpeg";
        userModel.BackgroundUrl = "https://storage.yandexcloud.net/id-gwalt-storage/image/BackgroundDefault.jpeg";
        
        if (!isDefaultImages)
        {
            var colorTop = _imageProcessor.GetRandomPastelColor();
            var colorBottom = _imageProcessor.GetSimilarPastelColor(colorTop);
        
            var initials = userModel.Name[..2].ToUpper();
            await using var avatar = _imageProcessor.GenerateAvatar(initials, 500, 500, 36 * 5, topColor: colorTop, bottomColor: colorBottom);
            await using var background = _imageProcessor.GenerateBackground(startColor: colorBottom, endColor: colorTop);

            userModel.AvatarUrl = await _fileService.UploadImageAsync(avatar, "image/png");
            userModel.BackgroundUrl = await _fileService.UploadImageAsync(background, "image/png");
        }
        
        var userEntity = _autoMapper.Map<User>(userModel);
        
        userEntity.PasswordHash = passwordHash;
        userEntity.PasswordSalt = salt;
        userEntity.RegistrationDate = DateTime.UtcNow;

        var id = await _userRepository.AddAsync(userEntity);

        return id;
    }

    public async Task<bool> UpdateAsync(UserModel user)
    {
        // Получаем текущую сущность из базы данных
        var existingUser = await _userRepository.GetByIdAsync(user.Id);
        if (existingUser == null)
        {
            return false;
        }

        // Обновляем только измененные поля
        _autoMapper.Map(user, existingUser);

        return await _userRepository.UpdateAsync(existingUser);
    }

    public async Task<bool> DeleteAsync(int id)
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