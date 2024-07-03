using System.IdentityModel.Tokens.Jwt;
using Sevriukoff.Gwalt.Application.Models;

namespace Sevriukoff.Gwalt.Application.Interfaces;

public interface IAuthService
{
    Task<bool> CheckEmailExistsAsync(string email);
    Task<(int userId, string accessToken, string refreshToken)> LoginAsync(string email, string password);
    Task<(string newAccessToken, string newRefreshToken)> RefreshTokenAsync(string refreshToken);
    Task<bool> Logout(int userId);
}