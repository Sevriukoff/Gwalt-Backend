namespace Sevriukoff.Gwalt.WebApi.Middleware;

public class DelayMiddleware
{
    private readonly RequestDelegate _next;
    private readonly int _delayMilliseconds;

    public DelayMiddleware(RequestDelegate next, int delayMilliseconds)
    {
        _next = next;
        _delayMilliseconds = delayMilliseconds;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        await Task.Delay(_delayMilliseconds);
        await _next(context);
    }
}

public static class DelayMiddlewareExtensions
{
    public static IApplicationBuilder UseDelayMiddleware(this IApplicationBuilder builder, int delayMilliseconds)
    {
        return builder.UseMiddleware<DelayMiddleware>(delayMilliseconds);
    }
}