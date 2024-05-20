using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Sevriukoff.Gwalt.WebApi.Common.Attributes;

public class CookieValueProviderFactory : IValueProviderFactory
{
    public Task CreateValueProviderAsync(ValueProviderFactoryContext context)
    {
        var cookies = context.ActionContext.HttpContext.Request.Cookies;

        context.ValueProviders.Add(new CookieValueProvider(ExtensionBindingSources.Cookie, cookies));
        
        return Task.CompletedTask;
    }
}