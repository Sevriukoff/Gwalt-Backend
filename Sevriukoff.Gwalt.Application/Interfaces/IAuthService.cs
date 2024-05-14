using System.IdentityModel.Tokens.Jwt;
using Sevriukoff.Gwalt.Application.Models;

namespace Sevriukoff.Gwalt.Application.Interfaces;

public interface IAuthService
{
    Task<(string accessToken, string refreshToken)> LoginAsync(string email, string password);
}