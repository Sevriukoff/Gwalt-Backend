using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Sevriukoff.Gwalt.Application.Enums;
using Sevriukoff.Gwalt.Application.Interfaces;
using Sevriukoff.Gwalt.WebApi.QueryParameters;

namespace Sevriukoff.Gwalt.WebApi.Controllers.Users;

[ApiController]
[Route("/api/v1/users/{userId}/likes")]
public class UserLikesController : ControllerBase
{
    private readonly ILikeService _likeService;
    private readonly IMapper _mapper;
    
    public UserLikesController(ILikeService likeService, IMapper mapper)
    {
        _likeService = likeService;
        _mapper = mapper;
    }
    
    // users/16/likes?type=track
    [HttpGet]
    public async Task<IActionResult> GetLikes(int userId , [FromQuery]LikeQueryParameters queryParameters)
    {
        var likeModels = await _likeService
            .GetAllByUserIdAsync(userId, queryParameters.Type, queryParameters.PageNumber, queryParameters.PageSize);
        
        var likesViewModel = _mapper.Map<IEnumerable<LikeTrackViewModel>>(likeModels);
        
        return Ok(likesViewModel);
    }
}

public class LikeTrackViewModel
{
    public int Id { get; set; }
    public string CoverUrl { get; set; }
    public AuthorViewModel[] Authors { get; set; }
    public string Title { get; set; }
    public DateTime ReleaseDate { get; set; }
    public int LikesCount { get; set; }
    public int ListensCount { get; set; }
    
    public class AuthorViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}