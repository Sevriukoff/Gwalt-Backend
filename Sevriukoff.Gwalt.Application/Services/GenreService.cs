using AutoMapper;
using Sevriukoff.Gwalt.Application.Interfaces;
using Sevriukoff.Gwalt.Application.Models;
using Sevriukoff.Gwalt.Infrastructure.Interfaces;

namespace Sevriukoff.Gwalt.Application.Services;

public class GenreService : IGenreService
{
    private readonly IGenreRepository _genreRepository;
    private readonly IMapper _mapper;

    public GenreService(IGenreRepository genreRepository, IMapper mapper)
    {
        _genreRepository = genreRepository;
        _mapper = mapper;
    }
    
    public async Task<IEnumerable<GenreModel>> GetAllAsync()
    {
        var entities = await _genreRepository.GetAllAsync(pageSize: 1000);
        
        return _mapper.Map<IEnumerable<GenreModel>>(entities);
    }
}