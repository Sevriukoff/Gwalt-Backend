namespace Sevriukoff.Gwalt.Application.Helpers;

public class JwtConfig
{
    public required string SecretKey { init; get; }
    public required string Issuer { init; get; }
    public required string Audience { init; get; }
    public required int AccessTokenExpiration { init; get; }
    public required int RefreshTokenExpiration { init; get; }
}