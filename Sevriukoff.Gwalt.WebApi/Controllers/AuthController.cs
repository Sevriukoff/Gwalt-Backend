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
    private readonly CookieConfig _cookieConfig;


    public AuthController(IAuthService authService, IOptions<JwtConfig> jwtSettings, IOptions<CookieConfig> cookieConfig)
    {
        _authService = authService;
        _jwtConfig = jwtSettings.Value;
        _cookieConfig = cookieConfig.Value;
    }
    
    [HttpPost("check-email")]
    public async Task<IActionResult> CheckEmail([FromBody] CheckEmailViewModel model)
    {
        await Task.Delay(2100);
        
        if (string.IsNullOrEmpty(model.Email))
        {
            return BadRequest("Email is required.");
        }

        try
        {
            var userExists = await _authService.CheckEmailExistsAsync(model.Email);

            return Ok(new { exists = userExists });
        }
        catch (Exception e)
        {
            return StatusCode(500, "Internal server error: " + e.Message);
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginViewModel user)
    {
        try
        {
            var (userId,accessToken, refreshToken) = await _authService.LoginAsync(user.Email, user.Password);
            
            Response.SetCookie(_cookieConfig.AccessToken, accessToken, TimeSpan.FromDays(1));
            Response.SetCookie(_cookieConfig.RefreshToken, refreshToken, TimeSpan.FromDays(1));
            
            return Ok( new {userId, accessToken, refreshToken});
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
            
            Response.SetCookie(_cookieConfig.AccessToken, newTokens.newAccessToken, TimeSpan.FromDays(1));
            Response.SetCookie(_cookieConfig.RefreshToken, newTokens.newRefreshToken, TimeSpan.FromDays(1));
        
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

public class CheckEmailViewModel
{
    public string Email { get; set; }
}