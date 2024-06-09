namespace Sevriukoff.Gwalt.WebApi.Common;

public class CookieHelper
{
    
}

public static class ResponseExtensions
{
    public static void SetCookie(this HttpResponse response, string key, string value, TimeSpan? expireTime)
    {
        var option = new CookieOptions();
        
        if (expireTime.HasValue)
        {
            option.HttpOnly = true;
            option.Expires = DateTime.Now.AddSeconds(expireTime.Value.TotalSeconds);
            option.SameSite = SameSiteMode.Strict;
        }
        
        response.Cookies.Append(key, value, option);
    }
}