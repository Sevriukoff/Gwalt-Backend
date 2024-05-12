using AutoMapper;
using Sevriukoff.Gwalt.Application.Interfaces;
using Sevriukoff.Gwalt.Application.Mapping;
using Sevriukoff.Gwalt.Application.Models;
using Sevriukoff.Gwalt.Application.Specification;
using Sevriukoff.Gwalt.Infrastructure.Entities;
using Sevriukoff.Gwalt.Infrastructure.Interfaces;

namespace Sevriukoff.Gwalt.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _autoMapper;
    
    public UserService(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _autoMapper = mapper;
    }
    public async Task<IEnumerable<UserModel>> GetAllAsync()
    {
        var spec = new IncludingSpecification<User>(
            "Albums",
            "Albums.Tracks",
            "Albums.Tracks.Genres",
            "Albums.Tracks.TotalLikes");
        var userModels = await _userRepository.GetAllAsync(spec);
        
        return userModels.Select(x => _autoMapper.Map<UserModel>(x));
    }
    
    public async Task<UserModel> GetByIdAsync(int id)
    {
        var userEntities = await _userRepository.GetByIdAsync(id);
        var userModel = _autoMapper.Map<UserModel>(userEntities);
        
        return userModel;
    }

    public async Task<IEnumerable<UserModel>> GetAllWithStaticsAsync()
    {
        var userEntities = await _userRepository.GetAllWithStaticsAsync();
        var userModels = userEntities.Select(x => _autoMapper.Map<UserModel>(x));

        return userModels;
    }

    public async Task<UserModel> AddAsync(UserModel user)
    {
        throw new NotImplementedException();
    }

    public async Task<UserModel> UpdateAsync(UserModel user)
    {
        throw new NotImplementedException();
    }

    public async Task<UserModel> DeleteAsync(int id)
    {
        throw new NotImplementedException();
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

    public int GetTracksCount(UserModel user)
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