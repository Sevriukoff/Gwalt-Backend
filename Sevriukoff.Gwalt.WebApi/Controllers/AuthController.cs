using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Sevriukoff.Gwalt.Application.Exceptions;
using Sevriukoff.Gwalt.Application.Helpers;
using Sevriukoff.Gwalt.Application.Interfaces;
using Sevriukoff.Gwalt.WebApi.Common;
using Sevriukoff.Gwalt.WebApi.Common.Attributes;
using Sevriukoff.Gwalt.WebApi.ViewModels;

namespace Sevriukoff.Gwalt.WebApi.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly JwtConfig _jwtConfig;


    public AuthController(IAuthService authService, IOptions<JwtConfig> jwtSettings)
    {
        _authService = authService;
        _jwtConfig = jwtSettings.Value;
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
    public async Task<IActionResult> RefreshToken([FromCookie("jwt-refresh")] string refreshToken)
    {
        try
        {
            if (string.IsNullOrEmpty(refreshToken))
                return Forbid();
            
            var newTokens = await _authService.RefreshTokenAsync(refreshToken);
            
            Response.SetCookie("jwt-access", newTokens.newAccessToken, 3600);
            Response.SetCookie("jwt-refresh", newTokens.newRefreshToken, 3600);
        
            return Ok();
        }
        catch (AuthException e)
        {
            return BadRequest(e.Message);
        }
    }

    [Authorize]
    [HttpPost("logout")]
    public async Task<IActionResult> Logout([FromJwtClaims("sub")] int userId)
    {
        await _authService.Logout(userId);
        
        return Ok();
    }
}