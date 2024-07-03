using Sevriukoff.Gwalt.Application.Models;
using Sevriukoff.Gwalt.Infrastructure.Entities;
using Sevriukoff.Gwalt.Infrastructure.Interfaces;

namespace Sevriukoff.Gwalt.Application.Interfaces;

public interface IAlbumService
{
    Task<IEnumerable<AlbumModel>> GetAllAsync(string[]? includes, string? orderBy, string[]? genres, int pageNumber, int pageSize);
    Task<AlbumModel> GetByIdAsync(int id, string[]? includes = null);
    Task<int> AddAsync(AlbumModel album);
}