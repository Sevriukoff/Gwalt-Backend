using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Sevriukoff.Gwalt.Application.Interfaces;
using Sevriukoff.Gwalt.Application.Models;
using Sevriukoff.Gwalt.WebApi.QueryParameters;
using Sevriukoff.Gwalt.WebApi.ViewModels;

namespace Sevriukoff.Gwalt.WebApi.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
public class TracksController : ControllerBase
{
    private readonly ITrackService _trackService;
    private readonly IMapper _mapper;

    public TracksController(ITrackService trackService, IMapper mapper)
    {
        _trackService = trackService;
        _mapper = mapper;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _trackService.GetAllAsync());
    }
    
    [HttpGet("{id:int}")]
    public async Task<ActionResult<TrackViewModel>> GetById(int id, [FromQuery] TrackQueryParameters queryParameters)
    {
        var includes = queryParameters.Includes?.Split(';');
        
        var track = await _trackService.GetByIdAsync(id, includes);
        
        var trackViewModel = _mapper.Map<TrackViewModel>(track);
        
        return Ok(trackViewModel);
    }
    
    [HttpPost]
    public async Task<IActionResult> Post(TrackCreateViewModel trackViewModel)
    {
        var trackModel = _mapper.Map<TrackModel>(trackViewModel);
        var id = await _trackService.AddAsync(trackModel);
        
        return CreatedAtAction(nameof(GetById), new {id = id}, id);
    }
}