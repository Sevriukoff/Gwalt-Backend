using Microsoft.AspNetCore.Mvc;
using Sevriukoff.Gwalt.Application.Interfaces;

namespace Sevriukoff.Gwalt.WebApi.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
public class TrackController : ControllerBase
{
    private readonly ITrackService _trackService;
    
    public TrackController(ITrackService trackService)
    {
        _trackService = trackService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _trackService.GetAllAsync());
    }
    
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        return Ok(await _trackService.GetByIdAsync(id));
    }
}