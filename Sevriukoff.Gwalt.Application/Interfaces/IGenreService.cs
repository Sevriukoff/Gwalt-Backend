using Sevriukoff.Gwalt.Application.Models;

namespace Sevriukoff.Gwalt.Application.Interfaces;

public interface IGenreService
{
    Task<IEnumerable<GenreModel>> GetAllAsync();
}