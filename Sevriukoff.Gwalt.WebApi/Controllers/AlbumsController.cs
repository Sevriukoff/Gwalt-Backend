using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Sevriukoff.Gwalt.Application.Interfaces;
using Sevriukoff.Gwalt.Application.Models;
using Sevriukoff.Gwalt.WebApi.QueryParameters;
using Sevriukoff.Gwalt.WebApi.ViewModels;

namespace Sevriukoff.Gwalt.WebApi.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
public class AlbumsController : ControllerBase
{
    private readonly IAlbumService _albumService;
    private readonly ITrackService _trackService;
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public AlbumsController(IAlbumService albumService, IMapper mapper, ITrackService trackService, IUserService userService)
    {
        _albumService = albumService;
        _mapper = mapper;
        _trackService = trackService;
        _userService = userService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] AlbumQueryParameters queryParameters)
    {
        var includes = queryParameters.Includes?.Split(';');
        var orderBy = queryParameters.OrderBy;
        var genres = queryParameters.Genres?.Split(';');
        
        var albumModels = await _albumService.GetAllAsync(includes, orderBy, genres, queryParameters.PageNumber,
            queryParameters.PageSize);
        
        var albumViewModels = _mapper.Map<List<AlbumViewModel>>(albumModels);
        
        return Ok(albumViewModels);
    }
    
    [HttpGet("{id:int}")]
    public async Task<ActionResult<AlbumGetByIdViewModel>> Get(int id, [FromQuery] AlbumQueryParameters queryParameters)
    {
        var includes = queryParameters.Includes?.Split(';');

        var albumModel = await _albumService.GetByIdAsync(id, includes);

        var albumViewModel = _mapper.Map<AlbumGetByIdViewModel>(albumModel);
        
        return Ok(albumViewModel);
    }
    
    [HttpGet("{id:int}/tracks")]
    public async Task<IActionResult> GetTracks(int id, [FromQuery] BaseQueryParameters queryParameters, [FromQuery] bool onlyId = false)
    {
        var includes = queryParameters.Includes?.Split(';');

        var trackModels = await _trackService.GetAllByAlbumIdAsync(id, includes, queryParameters.PageNumber,
            queryParameters.PageSize);

        return onlyId ? Ok(new {ids = trackModels.Select(x => x.Id).ToArray()}) : Ok(_mapper.Map<List<TrackFloatViewModel>>(trackModels));
    }
    
    [HttpGet("{id:int}/authors")]
    public async Task<IActionResult> GetAuthors(int id, [FromQuery] BaseQueryParameters queryParameters)
    {
        var includes = queryParameters.Includes?.Split(';');
        
        return Ok(await _userService.GetAllByAlbumIdAsync(id, includes, queryParameters.PageNumber,
            queryParameters.PageSize));
    }
    
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] AlbumCreateViewModel albumViewModel)
    {
        var albumModel = _mapper.Map<AlbumModel>(albumViewModel);
        var id = await _albumService.AddAsync(albumModel);
        
        return CreatedAtAction(nameof(Get), new {id = id}, id);
    }
}