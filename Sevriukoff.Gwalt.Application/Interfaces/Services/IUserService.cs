using Sevriukoff.Gwalt.Application.Models;

namespace Sevriukoff.Gwalt.Application.Interfaces;

public interface IUserService
{
    Task<IEnumerable<UserModel>> GetAllAsync(string[]? includes, string? orderBy, int pageNumber = 1,
        int pageSize = 10);
    Task<IEnumerable<UserModel?>> GetAllWithStaticsAsync(string? orderBy, int pageNumber = 1, int pageSize = 10);
    Task<IEnumerable<UserModel>> GetFollowersAsync(int userId, int pageNumber = 1, int pageSize = 10);
    Task<IEnumerable<UserModel>> GetFollowingsAsync(int userId, int pageNumber = 1, int pageSize = 10);
    Task<IEnumerable<UserModel>> GetAllByAlbumIdAsync(int albumId, string[]? includes = null, int pageNumber = 1, int pageSize = 10);
    Task<UserModel?> GetByIdAsync(int id, string[]? includes = null);
    
    Task<int> AddAsync(UserModel userModel, string password, bool isDefaultImages);
    Task<bool> UpdateAsync(UserModel user);
    Task<bool> DeleteAsync(int id);

    Task<int> FollowAsync(int userId, int followerId);
    Task<bool> UnfollowAsync(int userId, int followerId);
    Task<DateTime> GetFollowAsync(int userId, int followerId);

    #region Statics

    int GetTotalMetricsByTracks(UserModel user, Func<TrackModel, int> metricSelector);
    int GetTotalMetricsByAlbums(UserModel user, Func<AlbumModel, int> metricSelector);
    int GetTracksCount(UserModel? user);
    string[] GetAllGenres(UserModel user);

    #endregion
}