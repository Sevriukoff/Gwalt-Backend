using Sevriukoff.Gwalt.Application.Models;

namespace Sevriukoff.Gwalt.Application.Interfaces;

public interface IPlaylistService
{
    Task<IEnumerable<PlaylistModel>> GetAllAsync();
    Task<PlaylistModel> GetByIdAsync(int id);
    Task<int> AddAsync(PlaylistModel playlist);
    Task UpdateAsync(PlaylistModel playlist);
    Task DeleteAsync(int id);
}