using System.Collections;
using Sevriukoff.Gwalt.Application.Models;
using Sevriukoff.Gwalt.Infrastructure.Entities;

namespace Sevriukoff.Gwalt.Application.Interfaces;

public interface IUserService
{
    Task<IEnumerable<UserModel>> GetAllAsync();
    Task<UserModel> GetByIdAsync(int id);
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