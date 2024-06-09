using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using Sevriukoff.Gwalt.Application.Helpers;
using Sevriukoff.Gwalt.Application.Interfaces;
using Sevriukoff.Gwalt.Infrastructure.Extensions;

namespace Sevriukoff.Gwalt.Application.Services;

public class SessionService : ISessionService
{
    private readonly IDistributedCache _redisCache;
    private readonly JwtConfig _jwtConfig;

    public SessionService(IDistributedCache redisCache, IOptions<JwtConfig> jwtSettings)
    {
        _redisCache = redisCache;
        _jwtConfig = jwtSettings.Value;
    }

    public async Task AddSession(string sessionId, TimeSpan? expireTime)
    {
        await _redisCache.SetStringExAsync(sessionId, new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = expireTime
        });
    }
    
    public async Task AddTokenAsync(int userId, string token)
    {
        await _redisCache.SetStringExAsync(token, userId, new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(_jwtConfig.RefreshTokenExpiration)
        });

        var tokens = await _redisCache.GetObjectExAsync<List<string>>(userId);

        if (tokens == null)
            await _redisCache.SetObjectExAsync(userId, new List<string>{ token });
        else
        {
            tokens.Add(token);
            await _redisCache.SetObjectExAsync(userId, tokens);
        }
    }

    public bool TryGetUserId(string token, out int userId)
    {
        userId = -1;
    
        var result = _redisCache.GetString(token);
        
        return !string.IsNullOrEmpty(result) && int.TryParse(result, out userId);
    }


    public async Task<(bool success, int userId)> TryGetUserIdAsync(string token)
    {
        var result = await _redisCache.GetStringAsync(token);
        
        return string.IsNullOrEmpty(result) ? (false, -1) : (true, int.Parse(result));
    }

    public async Task<bool> RemoveTokenAsync(string token)
    {
        var (success, userId) = await TryGetUserIdAsync(token);

        if (!success)
            return false;
        
        await _redisCache.RemoveAsync(token);
        
        var tokens = await _redisCache.GetObjectExAsync<List<string>>(userId);

        if (tokens == null)
            return false;
        
        tokens.Remove(token);
        await _redisCache.SetObjectExAsync(userId, tokens);

        return true;
    }

    public async Task<bool> RemoveAllTokensAsync(int userId)
    {
        var tokens = await _redisCache.GetObjectExAsync<List<string>>(userId);
        await _redisCache.RemoveExAsync(userId);
        
        if (tokens == null)
            return false;

        foreach (var token in tokens)
        {
            await _redisCache.RemoveAsync(token);
        }

        return true;
    }
}