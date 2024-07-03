using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Sevriukoff.Gwalt.Application.Enums;
using Sevriukoff.Gwalt.Application.Interfaces;
using Sevriukoff.Gwalt.WebApi.QueryParameters;
using Sevriukoff.Gwalt.WebApi.ViewModels;

namespace Sevriukoff.Gwalt.WebApi.Controllers;

[ApiController]
[Route("/api/v1/users/{userId}/listens")]
public class UserListensController : ControllerBase
{
    private readonly IListenService _listenService;
    private readonly IMapper _mapper;
    
    public UserListensController(IListenService listenService, IMapper mapper)
    {
        _listenService = listenService;
        _mapper = mapper;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetListens(string userId, [FromQuery] BaseQueryParameters queryParameters)
    {
        var includes = queryParameters.Includes?.Split(';');
        
        var listenModels = int.TryParse(userId, out var id)
            ? await _listenService
                .GetListensByUserIdAsync(ListenableType.Track, id, includes, queryParameters.PageNumber, queryParameters.PageSize)
            : await _listenService
                .GetListensBySessionIdAsync(ListenableType.Track, userId, includes, queryParameters.PageNumber, queryParameters.PageSize);

        var listensViewModel = _mapper.Map<IEnumerable<ListenTrackViewModel>>(listenModels);  
        
        return Ok(listensViewModel);
    }
}