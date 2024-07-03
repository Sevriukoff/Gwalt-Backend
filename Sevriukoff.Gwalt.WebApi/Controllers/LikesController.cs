using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sevriukoff.Gwalt.Application.Enums;
using Sevriukoff.Gwalt.Application.Interfaces;
using Sevriukoff.Gwalt.WebApi.Common.Attributes;
using Sevriukoff.Gwalt.WebApi.ViewModels;

namespace Sevriukoff.Gwalt.WebApi.Controllers;

[Authorize]
[ApiController]
[Route("api/v1/[controller]")]
public class LikesController : ControllerBase
{
    private readonly ILikeService _likeService;
    
    public LikesController(ILikeService likeService)
    {
        _likeService = likeService;
    }
    
    [HttpGet("{likeableType}/{likeableId:int}")]
    public async Task<IActionResult> Get([FromJwtClaims("sub")] int userId, LikeableType likeableType, int likeableId)
    {
        var like = await _likeService.GetAsync(likeableType, likeableId, userId);
        
        if (like == null)
            return NotFound();
        
        return Ok(like);
    }
    
    [HttpPost]
    public async Task<IActionResult> Post([FromJwtClaims("sub")] int userId, [FromBody] LikeCreateViewModel model)
    {
        var id = await _likeService.AddAsync(model.LikeableType, model.LikeableId, userId);
        
        return CreatedAtAction(nameof(Get), new {likeableType = model.LikeableType.ToString(), likeableId = id}, new {id = id});
    }
    
    [HttpDelete("{likeId:int}")]
    public async Task<IActionResult> Delete(int likeId)
    {
        await _likeService.DeleteAsync(likeId);
        
        return Ok();
    }
}