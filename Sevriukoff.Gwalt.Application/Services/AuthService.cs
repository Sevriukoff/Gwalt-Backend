using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using Sevriukoff.Gwalt.Application.Exceptions;
using Sevriukoff.Gwalt.Application.Helpers;
using Sevriukoff.Gwalt.Application.Interfaces;
using Sevriukoff.Gwalt.Infrastructure.Extensions;
using Sevriukoff.Gwalt.Infrastructure.Interfaces;

namespace Sevriukoff.Gwalt.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly JwtHelper _jwtHelper;
    private readonly PasswordHasher _passwordHasher;
    private readonly ISessionService _sessionService;

    public AuthService(IUserRepository userRepository,
        JwtHelper jwtHelper,
        PasswordHasher passwordHasher,
        ISessionService sessionService)
    {
        _userRepository = userRepository;
        _jwtHelper = jwtHelper;
        _passwordHasher = passwordHasher;
        _sessionService = sessionService;
    }
    
    public async Task<(string accessToken, string refreshToken)> LoginAsync(string email, string password)
    {
        var userEntity = await _userRepository.GetByEmailAsync(email);
        
        var isVerified = _passwordHasher.VerifyPassword(password, userEntity.PasswordSalt, userEntity.PasswordHash);

        if (!isVerified)
            throw new AuthException("Invalid email or password");

        var (accessToken, refreshToken) = _jwtHelper.GenerateTokens(userEntity.Id);
        
        await _sessionService.AddTokenAsync(userEntity.Id, refreshToken);
        
        return (accessToken, refreshToken);
    }

    public async Task<(string newAccessToken, string newRefreshToken)> RefreshTokenAsync(string refreshToken)
    {
        var (success, userId) = await _sessionService.TryGetUserIdAsync(refreshToken);

        if (!success)
        {
            throw new AuthException(
                "Invalid refresh token. The token may have been compromised. We recommend that you re-authenticate");
        }
        
        var (newAccessToken, newRefreshToken) = _jwtHelper.GenerateTokens(userId);

        await _sessionService.RemoveTokenAsync(refreshToken);
        await _sessionService.AddTokenAsync(userId, newRefreshToken);
        
        return (newAccessToken, newRefreshToken);
    }

    public async Task<bool> Logout(int userId)
    {
        return await _sessionService.RemoveAllTokensAsync(userId);
    }
}