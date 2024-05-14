using Microsoft.AspNetCore.Mvc;
using Sevriukoff.Gwalt.Application.Exceptions;
using Sevriukoff.Gwalt.Application.Helpers;
using Sevriukoff.Gwalt.Application.Interfaces;
using Sevriukoff.Gwalt.WebApi.ViewModels;

namespace Sevriukoff.Gwalt.WebApi.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    
    
    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost]
    public async Task<IActionResult> Login([FromBody] UserLoginViewModel user)
    {
        try
        {
            var tokens = await _authService.LoginAsync(user.Email, user.Password);
        
            return Ok(tokens);
        }
        catch (AuthException e)
        {
            return BadRequest();
        }
    }
}