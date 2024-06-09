using Microsoft.Extensions.Options;
using Sevriukoff.Gwalt.Application.Interfaces;
using Sevriukoff.Gwalt.WebApi.Common;

namespace Sevriukoff.Gwalt.WebApi.Middleware;

public class PublicSessionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ISessionService _sessionService;
    private readonly CookieConfig _cookieConfig;
    
    public PublicSessionMiddleware(RequestDelegate next, ISessionService sessionService, IOptions<CookieConfig> cookieConfig)
    {
        _next = next;
        _sessionService = sessionService;
        _cookieConfig = cookieConfig.Value;
    }
    
    public async Task InvokeAsync(HttpContext context)
    {
        var cookies = context.Request.Cookies;
        
        if (!cookies.ContainsKey(_cookieConfig.AccessToken) &&
            !cookies.ContainsKey(_cookieConfig.RefreshToken) &&
            !cookies.ContainsKey(_cookieConfig.SessionId))
        {
            var sessionId = Guid.NewGuid().ToString();
            var expires = TimeSpan.FromSeconds(_cookieConfig.SessionIdExpiration);

            await _sessionService.AddSession(sessionId, expires);
            
            context.Response.SetCookie(_cookieConfig.SessionId, sessionId, expires);
        }
        
        await _next(context);
    }
}

public static class PublicSessionMiddlewareExtensions
{
    public static IApplicationBuilder UsePublicSessionMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<PublicSessionMiddleware>();
    }
}