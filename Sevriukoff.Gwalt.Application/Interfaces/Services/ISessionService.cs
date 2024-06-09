namespace Sevriukoff.Gwalt.Application.Interfaces;

public interface ISessionService
{
    Task AddSession(string sessionId, TimeSpan? expireTime);
    Task AddTokenAsync(int userId, string token);
    bool TryGetUserId(string token, out int userId);
    Task<(bool success, int userId)> TryGetUserIdAsync(string token);
    Task<bool> RemoveTokenAsync(string token);
    Task<bool> RemoveAllTokensAsync(int userId);
}