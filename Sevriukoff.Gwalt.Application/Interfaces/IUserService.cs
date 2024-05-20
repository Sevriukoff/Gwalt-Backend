using Sevriukoff.Gwalt.Application.Models;

namespace Sevriukoff.Gwalt.Application.Interfaces;

public interface IUserService
{
    Task<IEnumerable<UserModel>> GetAllAsync(string[]? includes, string? orderBy);
    Task<UserModel> GetByIdAsync(int id, string[]? includes);
    Task<IEnumerable<UserModel>> GetAllWithStaticsAsync();
    
    Task<int> AddAsync(string name, string email, string password);
    Task<UserModel> UpdateAsync(UserModel user);
    Task<UserModel> DeleteAsync(int id);

    #region Statics

    int GetTotalMetricsByTracks(UserModel user, Func<TrackModel, int> metricSelector);
    int GetTotalMetricsByAlbums(UserModel user, Func<AlbumModel, int> metricSelector);
    int GetTracksCount(UserModel user);
    string[] GetAllGenres(UserModel user);

    #endregion
}