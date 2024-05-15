using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sevriukoff.Gwalt.Application.Exceptions;
using Sevriukoff.Gwalt.Application.Interfaces;
using Sevriukoff.Gwalt.WebApi.Common;
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

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginViewModel user)
    {
        try
        {
            var (accessToken, refreshToken) = await _authService.LoginAsync(user.Email, user.Password);
            
            Response.SetCookie("jwt-access", accessToken, 3600);
            Response.SetCookie("jwt-refresh", refreshToken, 3600);
            
            return Ok();
        }
        catch (AuthException e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpPost("refresh")]
    public async Task<IActionResult> RefreshToken()
    {
        try
        {
            var result = Request.Cookies.TryGetValue("jwt-refresh", out var refreshToken);

            if (!result)
                return Forbid();
            
            var newTokens = await _authService.RefreshTokenAsync(refreshToken);
        
            return Ok(newTokens);
        }
        catch (AuthException e)
        {
            return BadRequest(e.Message);
        }
    }

    [Authorize]
    [HttpPost("logout")]
    public async Task<IActionResult> Logout(int userId)
    {
        await _authService.Logout(userId);
        
        return Ok();
    }
}