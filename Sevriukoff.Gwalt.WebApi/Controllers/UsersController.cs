using Microsoft.AspNetCore.Mvc;
using Sevriukoff.Gwalt.Application.Interfaces;
using Sevriukoff.Gwalt.Infrastructure.Entities;
using Sevriukoff.Gwalt.WebApi.QueryParameters;

namespace Sevriukoff.Gwalt.WebApi.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;
    
    public UsersController(IUserService userService)
    {
        _userService = userService;
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetAll([FromQuery] UserQueryParameterses queryParameterses)
    {
        if (queryParameterses.WithStat)
        {
            return Ok(await _userService.GetAllWithStaticsAsync());
        }
        
        return Ok(await _userService.GetAllAsync());
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        return Ok(await _userService.GetByIdAsync(id));
    }
    
    [HttpPost]
    public async Task<IActionResult> Post(User user)
    {
        return Ok(await _userService.AddAsync(user));
    }
    
    [HttpPut]
    public async Task<IActionResult> Put(User user)
    {
        return Ok(await _userService.UpdateAsync(user));
    }
    
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        return Ok(await _userService.DeleteAsync(id));
    }
}