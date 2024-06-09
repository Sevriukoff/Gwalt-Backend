using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Sevriukoff.Gwalt.Application.Enums;
using Sevriukoff.Gwalt.Application.Interfaces;
using Sevriukoff.Gwalt.Application.Models;
using Sevriukoff.Gwalt.Infrastructure.Entities;
using Sevriukoff.Gwalt.WebApi.Common.Attributes;
using Sevriukoff.Gwalt.WebApi.ViewModels;

namespace Sevriukoff.Gwalt.WebApi.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class ListensController : ControllerBase
{
    private readonly IListenService _listenService;
    private readonly IMapper _mapper;
    
    public ListensController(IListenService listenService, IMapper mapper)
    {
        _listenService = listenService;
        _mapper = mapper;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok();
    }
    
    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        return Ok();
    }
    
    [HttpPost]
    public async Task<IActionResult> Post([FromJwtClaims("sub")] int userId, [FromBody] ListenCreateViewModel model)
    {
        Request.Cookies.TryGetValue("session-id", out var sessionId);

        IListenable listenable = model.ListenableType == ListenableType.Track
            ? _mapper.Map<TrackModel>(model)
            : _mapper.Map<AlbumModel>(model);

        var listenModel = _mapper.Map<ListenModel>(model);
        listenModel.User = new UserModel(userId);
        listenModel.SessionId = sessionId;
        listenModel.Listenable = listenable;

        var id = await _listenService.AddAsync(listenModel);

        return CreatedAtAction(nameof(Get), new { id = id }, new { id = id });
    }
}