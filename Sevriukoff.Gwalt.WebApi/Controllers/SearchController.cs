using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Sevriukoff.Gwalt.Application.Interfaces;
using Sevriukoff.Gwalt.WebApi.ViewModels;

namespace Sevriukoff.Gwalt.WebApi.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class SearchController : ControllerBase
{
    private readonly ISearchService _searchService;
    private readonly IMapper _mapper;

    public SearchController(ISearchService searchService, IMapper mapper)
    {
        _searchService = searchService;
        _mapper = mapper;
    }
    
    [HttpGet]
    public async Task<IActionResult> Search([FromQuery] string query)
    {
        if (string.IsNullOrWhiteSpace(query))
        {
            return BadRequest("Query parameter is required.");
        }

        var results = await _searchService.SearchAsync(query);
        
        return Ok(new
        {
            albums = _mapper.Map<AlbumSearchViewModel[]>(results.albumModels),
            tracks = _mapper.Map<TrackSearchViewModel[]>(results.trackModels),
            users = _mapper.Map<UserViewModel[]>(results.userModels)
        });
    }
}