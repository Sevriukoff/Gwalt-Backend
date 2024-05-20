using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Sevriukoff.Gwalt.Application.Helpers;

public class JwtHelper
{
    private readonly JwtConfig _jwtConfig;
    
    public JwtHelper(IOptions<JwtConfig> jwtSettings)
    {
        _jwtConfig = jwtSettings.Value;
    }
    
    public (string accessToken, string refreshToken) GenerateTokens(int userId)
    {
        var claims = new List<Claim>
        {
            new (JwtRegisteredClaimNames.Sub,userId.ToString())
        };
        
        var notBefore = DateTime.Now;
        var accessTokenExpires = notBefore.AddSeconds(_jwtConfig.AccessTokenExpiration);
        
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig.SecretKey));
        var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var accessToken = new JwtSecurityToken(
            _jwtConfig.Issuer,
            _jwtConfig.Audience,
            claims,
            notBefore,
            accessTokenExpires,
            signingCredentials
        );

        var accessTokenString = new JwtSecurityTokenHandler().WriteToken(accessToken);
        var refreshTokenString = Guid.NewGuid().ToString();
        
        return (accessTokenString, refreshTokenString);
    }
    
    public IEnumerable<Claim> GetClaims(string jwtToken)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var claimsPrincipal = tokenHandler.ReadJwtToken(jwtToken);
            
            return claimsPrincipal.Claims;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при декодировании и верификации токена: {ex.Message}");
            return Enumerable.Empty<Claim>();
        }
    }
}