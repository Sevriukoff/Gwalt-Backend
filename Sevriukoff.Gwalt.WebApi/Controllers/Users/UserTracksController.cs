using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Sevriukoff.Gwalt.Application.Helpers;
using Sevriukoff.Gwalt.Application.Interfaces;
using Sevriukoff.Gwalt.WebApi.QueryParameters;
using Sevriukoff.Gwalt.WebApi.ViewModels;

namespace Sevriukoff.Gwalt.WebApi.Controllers;

[ApiController]
[Route("/api/v1/users/{userId:int}/tracks")]
public class UserTracksController : ControllerBase
{
    private readonly ITrackService _trackService;
    private readonly DateTimeHelper _dateTimeHelper;
    private readonly IMapper _mapper;

    public UserTracksController(ITrackService trackService, IMapper mapper, DateTimeHelper dateTimeHelper)
    {
        _trackService = trackService;
        _mapper = mapper;
        _dateTimeHelper = dateTimeHelper;
    }

    [HttpGet]
    public async Task<IActionResult> GetTracks(int userId, [FromQuery] TrackQueryParameters queryParameters)
    {
        var includes = queryParameters.Includes?.Split(';');
        
        var tracksModel = await _trackService.GetByUserIdAsync(userId, includes, queryParameters.PageNumber, queryParameters.PageSize);
        
        return Ok(_mapper.Map<IEnumerable<TrackFloatViewModel>>(tracksModel));
    }
}