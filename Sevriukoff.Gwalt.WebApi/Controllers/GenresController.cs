using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Sevriukoff.Gwalt.Application.Interfaces;
using Sevriukoff.Gwalt.WebApi.ViewModels;

namespace Sevriukoff.Gwalt.WebApi.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
public class GenresController : ControllerBase
{
    private readonly IGenreService _genreService;
    private readonly IMapper _mapper;

    public GenresController(IGenreService genreService, IMapper mapper)
    {
        _genreService = genreService;
        _mapper = mapper;
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<GenreViewModel>>> GetAll()
    {
        var genresModel = await _genreService.GetAllAsync();
        var genresViewModel = genresModel.Select(_mapper.Map<GenreViewModel>);
        
        return Ok(genresViewModel);
    }
}