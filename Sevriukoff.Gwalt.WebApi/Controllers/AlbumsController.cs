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
    private readonly IMapper _mapper;

    public AlbumsController(IAlbumService albumService, IMapper mapper)
    {
        _albumService = albumService;
        _mapper = mapper;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] AlbumQueryParameters queryParameters)
    {
        var includes = queryParameters.Includes?.Split(';');
        var orderBy = queryParameters.OrderBy;
        
        return Ok(await _albumService.GetAllAsync(includes, orderBy));
    }
    
    [HttpGet("{id:int}")]
    public async Task<ActionResult<AlbumViewModel>> Get(int id, [FromQuery] AlbumQueryParameters queryParameters)
    {
        var includes = queryParameters.Includes?.Split(';');
        
        return Ok(await _albumService.GetByIdAsync(id, includes));
    }
    
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] AlbumCreateViewModel albumViewModel)
    {
        var albumModel = _mapper.Map<AlbumModel>(albumViewModel);
        var id = await _albumService.AddAsync(albumModel);
        
        return CreatedAtAction(nameof(Get), new {id = id}, id);
    }
    
    /*
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _albumService.GetAllAsync());
    }
    
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        return Ok(await _albumService.GetByIdAsync(id));
    }
    
    [HttpGet("{id:int}/tracks")]
    public async Task<IActionResult> GetTracks(int id)
    {
        return Ok(await _albumService.GetTracksAsync(id));
    }
    
    [HttpGet("{id:int}/genres")]
    public async Task<IActionResult> GetGenres(int id)
    {
        return Ok(await _albumService.GetGenresAsync(id));
    }
    
    [HttpGet("{id:int}/artists")]
    public async Task<IActionResult> GetArtists(int id)
    {
        return Ok(await _albumService.GetArtistsAsync(id));
    }
    
    [HttpGet("{id:int}/users")]
    public async Task<IActionResult> GetUsers(int id)
    {
        return Ok(await _albumService.GetUsersAsync(id));
    }
    
    [HttpGet("{id:int}/comments")]
    public async Task<IActionResult> GetComments(int id)
    {
        return Ok(await _albumService.GetCommentsAsync(id));
    }
    
    [HttpGet("{id:int}/likes")]
    public async Task<IActionResult> GetLikes(int id)
    {
        return Ok(await _albumService.GetLikesAsync(id));
    }
    
    [HttpGet("{id:int}/listens")]
    public async Task<IActionResult> GetListens(int id)
    {
        return Ok(await _albumService.GetListensAsync(id));
    }
    
    [HttpGet("{id:int}/stats")]
    public async Task<IActionResult> GetStats(int id)
    {
        return Ok(await _albumService.GetStatsAsync(id));
    }
    
    [HttpGet("{id:int}/tags")]
    public async Task<IActionResult> GetTags(int id)
    {
        return Ok(await _albumService.GetTagsAsync(id));
    }
    
    [HttpGet("{id:int}/tracks/{trackId:int}")]
    public async Task<IActionResult> GetTrack(int id, int trackId)
    {
        return Ok(await _albumService.GetTrackAsync(id, trackId));
    }

    [HttpGet("{id:int}/users/{userId:int}")]
    public async Task<IActionResult> GetUser(int id, int userId)
    {
        return Ok(await _albumService.GetUserAsync(id, userId));
    }
    
    [HttpGet("{id:int}/comments/{commentId:int}")]
    public async Task<IActionResult> GetComment(int id, int commentId)
    {
        return Ok(await _albumService.GetCommentAsync(id, commentId));
    }

    [HttpGet("{id:int}/likes/{likeId:int}")]
    public async Task<IActionResult> GetLike(int id, int likeId)
    {
        return Ok(await _albumService.GetLikeAsync(id, likeId));
    }
    
    [HttpGet("{id:int}/listens/{listenId:int}")]
    public async Task<IActionResult> GetListen(int id, int listenId)
    {
        return Ok(await _albumService.GetListenAsync(id, listenId));
    }
    
    [HttpGet("{id:int}/stats/{statId:int}")]
    public async Task<IActionResult> GetStat(int id, int statId)
    {
        return Ok(await _albumService.GetStatAsync(id, statId));
    }
    
    [HttpGet("{id:int}/tags/{tagId:int}")]
    public async Task<IActionResult> GetTag(int id, int tagId)
    {
        return Ok(await _albumService.GetTagAsync(id, tagId));
    }
    
    [HttpGet("{id:int}/tracks/{trackId:int}/comments")]
    public async Task<IActionResult> GetTrackComments(int id, int trackId)
    {
        return Ok(await _albumService.GetTrackCommentsAsync(id, trackId));
    }
    
    [HttpGet("{id:int}/tracks/{trackId:int}/likes")]
    public async Task<IActionResult> GetTrackLikes(int id, int trackId)
    {
        return Ok(await _albumService.GetTrackLikesAsync(id, trackId));
    }
    
    [HttpGet("{id:int}/tracks/{trackId:int}/listens")]
    public async Task<IActionResult> GetTrackListens(int id, int trackId)
    {
        return Ok(await _albumService.GetTrackListensAsync(id, trackId));
    }
    
    [HttpGet("{id:int}/tracks/{trackId:int}/stats")]
    public async Task<IActionResult> GetTrackStats(int id, int trackId)
    {
        return Ok(await _albumService.GetTrackStatsAsync(id, trackId));
    }
    
    [HttpGet("{id:int}/tracks/{trackId:int}/tags")]
    public async Task<IActionResult> GetTrackTags(int id, int trackId)
    {
        return Ok(await _albumService.GetTrackTagsAsync(id, trackId));
    }
    
    [HttpGet("{id:int}/users/{userId:int}/comments")]
    public async Task<IActionResult> GetUserComments(int id, int userId)
    {
        return Ok(await _albumService.GetUserCommentsAsync(id, userId));
    }
    
    [HttpGet("{id:int}/users/{userId:int}/likes")]
    public async Task<IActionResult> GetUserLikes(int id, int userId)
    {
        return Ok(await _albumService.GetUserLikesAsync(id, userId));
    }
    
    [HttpGet("{id:int}/users/{userId:int}/listens")]
    public async Task<IActionResult> GetUserListens(int id, int userId)
    {
        return Ok(await _albumService.GetUserListensAsync(id, userId));
    }
    
    [HttpGet("{id:int}/users/{userId:int}/stats")]
    public async Task<IActionResult> GetUserStats(int id, int userId)
    {
        return Ok(await _albumService.GetUserStatsAsync(id, userId));
    }
    
    [HttpGet("{id:int}/users/{userId:int}/tags")]
    public async Task<IActionResult> GetUserTags(int id, int userId)
    {
        return Ok(await _albumService.GetUserTagsAsync(id, userId));
    }
    */
}