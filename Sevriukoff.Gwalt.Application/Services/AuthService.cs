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
    private readonly IDistributedCache _redisCache;
    private readonly JwtSettings _jwtSettings;

    public AuthService(IUserRepository userRepository,
        JwtHelper jwtHelper,
        PasswordHasher passwordHasher,
        IDistributedCache redisCache,
        IOptions<JwtSettings> jwtSettings)
    {
        _userRepository = userRepository;
        _jwtHelper = jwtHelper;
        _passwordHasher = passwordHasher;
        _redisCache = redisCache;
        _jwtSettings = jwtSettings.Value;
    }
    
    public async Task<(string accessToken, string refreshToken)> LoginAsync(string email, string password)
    {
        var userEntity = await _userRepository.GetByEmailAsync(email);
        
        var isVerified = _passwordHasher.VerifyPassword(password, userEntity.PasswordSalt, userEntity.PasswordHash);

        if (!isVerified)
            throw new AuthException("Invalid email or password");

        var (accessToken, refreshToken) = _jwtHelper.GenerateTokens(userEntity.Id);

        await _redisCache.SetStringExAsync(refreshToken, userEntity.Id, new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(_jwtSettings.RefreshTokenExpiration)
        });

        var refreshTokens = await _redisCache.GetObjectExAsync<List<string>>(userEntity.Id);

        if (refreshTokens == null)
            await _redisCache.SetObjectExAsync(userEntity.Id, new List<string>{ refreshToken });
        else
        {
            refreshTokens.Add(refreshToken);
            await _redisCache.SetObjectExAsync(userEntity.Id, refreshTokens);
        }
        
        return (accessToken, refreshToken);
    }

    public async Task<(string newAccessToken, string newRefreshToken)> RefreshTokenAsync(string refreshToken)
    {
        var userId = await _redisCache.GetObjectAsync<int>(refreshToken);
        await _redisCache.RemoveAsync(refreshToken);
        
        var refreshTokens = await _redisCache.GetObjectExAsync<List<string>>(userId);

        if (refreshTokens == null)
            throw new AuthException("Invalid refresh token");
        
        refreshTokens.Remove(refreshToken);
        
        var (newAccessToken, newRefreshToken) = _jwtHelper.GenerateTokens(userId);
        
        await _redisCache.SetStringExAsync(newRefreshToken, userId, new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(_jwtSettings.RefreshTokenExpiration)
        });
        
        refreshTokens.Add(newRefreshToken);
        
        await _redisCache.SetObjectExAsync(userId, refreshTokens);
        
        return (newAccessToken, newRefreshToken);
    }

    public async Task<bool> Logout(int userId)
    {
        var refreshTokens = await _redisCache.GetObjectExAsync<List<string>>(userId);

        foreach (var token in refreshTokens!)
        {
            await _redisCache.RemoveAsync(token);
        }
        
        await _redisCache.RemoveExAsync(userId);

        return true;
    }
}