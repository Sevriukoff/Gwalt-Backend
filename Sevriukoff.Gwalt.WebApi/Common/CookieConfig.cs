namespace Sevriukoff.Gwalt.WebApi.Common;

public class CookieConfig
{
    public required string AccessToken { get; set; }
    public required string RefreshToken { get; set; }
    public required string SessionId { get; set; }
    
    public required int SessionIdExpiration { get; set; }
}