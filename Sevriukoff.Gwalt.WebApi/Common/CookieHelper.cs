namespace Sevriukoff.Gwalt.WebApi.Common;

public class CookieHelper
{
    
}

public static class ResponseExtensions
{
    public static void SetCookie(this HttpResponse response, string key, string value, int? expireTime)
    {
        var option = new CookieOptions();
        
        if (expireTime.HasValue)
        {
            option.HttpOnly = true;
            option.Expires = DateTime.Now.AddSeconds(expireTime.Value);
        }
        
        response.Cookies.Append(key, value, option);
    }
}