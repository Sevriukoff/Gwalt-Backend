using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sevriukoff.Gwalt.Application.Interfaces;
using Sevriukoff.Gwalt.Application.Models;
using Sevriukoff.Gwalt.WebApi.Common.Attributes;
using Sevriukoff.Gwalt.WebApi.ViewModels;

namespace Sevriukoff.Gwalt.WebApi.Controllers;

[Authorize]
[ApiController]
[Route("api/v1/[controller]")]
public class PlaylistsController : ControllerBase
{
    private readonly IPlaylistService _playlistService;
    
    public PlaylistsController(IPlaylistService playlistService)
    {
        _playlistService = playlistService;
    }
    
    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _playlistService.GetAllAsync());
    }
    
    [AllowAnonymous]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        return Ok(await _playlistService.GetByIdAsync(id));
    }
    
    [HttpPost]
    public async Task<IActionResult> Post([FromJwtClaims("sub")] int userId,
        [FromBody] PlaylistViewModel playlistViewModel)
    {
        var model = new PlaylistModel()
        {
            Title = playlistViewModel.Title,
            Description = playlistViewModel.Description,
            CoverUrl = playlistViewModel.CoverUrl,
            IsPrivate = playlistViewModel.IsPrivate,
            CreatedAt = DateTime.Now,
            User = new UserModel(userId)
        };
        
        var id = await _playlistService.AddAsync(model);
        
        return Ok(id);
    }
    
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Put(int id, [FromBody] PlaylistViewModel playlistViewModel)
    {
        var model = new PlaylistModel();
        
        await _playlistService.UpdateAsync(model);
        
        return Ok();
    }
    
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _playlistService.DeleteAsync(id);
        
        return Ok();
    }
}