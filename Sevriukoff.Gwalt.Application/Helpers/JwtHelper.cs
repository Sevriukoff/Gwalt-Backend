using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Sevriukoff.Gwalt.Application.Models;

namespace Sevriukoff.Gwalt.Application.Helpers;

public class JwtHelper
{
    private readonly JwtSettings _jwtSettings;
    
    public JwtHelper(IOptions<JwtSettings> jwtSettings)
    {
        _jwtSettings = jwtSettings.Value;
    }
    
    public (string accessToken, string refreshToken) GenerateTokens(UserModel user)
    {
        var claims = new List<Claim>
        {
            new (JwtRegisteredClaimNames.Sub,user.Id.ToString()),
            new (JwtRegisteredClaimNames.Email, user.Email)
        };
        
        var notBefore = DateTime.Now;
        var accessTokenExpires = notBefore.AddSeconds(_jwtSettings.AccessTokenExpiration);
        var refreshTokenExpires = notBefore.AddSeconds(_jwtSettings.RefreshTokenExpiration);
        
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
        var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var accessToken = new JwtSecurityToken(
            _jwtSettings.Issuer,
            _jwtSettings.Audience,
            claims,
            notBefore,
            accessTokenExpires,
            signingCredentials
        );

        var refreshToken = new JwtSecurityToken(
            _jwtSettings.Issuer,
            _jwtSettings.Audience,
            claims,
            notBefore,
            refreshTokenExpires,
            signingCredentials
        );

        var accessTokenString = new JwtSecurityTokenHandler().WriteToken(accessToken);
        var refreshTokenString = new JwtSecurityTokenHandler().WriteToken(refreshToken);
        
        return (accessTokenString, refreshTokenString);
    }
}