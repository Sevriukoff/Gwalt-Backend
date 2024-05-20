using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sevriukoff.Gwalt.Application.Enums;
using Sevriukoff.Gwalt.Application.Interfaces;
using Sevriukoff.Gwalt.WebApi.Common.Attributes;

namespace Sevriukoff.Gwalt.WebApi.Controllers;

[Authorize]
[ApiController]
[Route("api/v1/[controller]")]
public class LikeController : ControllerBase
{
    private readonly ILikeService _likeService;
    
    public LikeController(ILikeService likeService)
    {
        _likeService = likeService;
    }

    /// <summary>
    /// POST /api/v1/like/
    /// </summary>
    /// <returns></returns>
    [HttpPost()]
    public async Task<IActionResult> Post([FromJwtClaims("sub")] int userId, [FromBody] LikeCreateViewModel model)
    {
        var id = await _likeService.AddAsync(model.LikeableType,model.LikeableId, userId);
        
        return Ok(id);
    }
    
    [HttpDelete("{likeId:int}")]
    public async Task<IActionResult> Delete(int likeId)
    {
        await _likeService.DeleteAsync(likeId);
        
        return Ok();
    }
}

public class LikeCreateViewModel
{
    public LikeableType LikeableType { get; set; }
    public int LikeableId { get; set; }
}