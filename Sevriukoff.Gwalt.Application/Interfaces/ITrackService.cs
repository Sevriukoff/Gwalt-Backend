using Sevriukoff.Gwalt.Infrastructure.Entities;

namespace Sevriukoff.Gwalt.Application.Interfaces;

public interface ITrackService
{
    Task<IEnumerable<Track>> GetAllAsync();
    Task<Track?> GetByIdAsync(int id);
}