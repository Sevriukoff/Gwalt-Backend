using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Sevriukoff.Gwalt.WebApi.Common.Attributes;

public class CookieValueProvider : BindingSourceValueProvider
{
    private IRequestCookieCollection Cookies { get; }
    
    public CookieValueProvider(BindingSource bindingSource, IRequestCookieCollection cookies) : base(bindingSource)
    {
        Cookies = cookies;
    }

    public override bool ContainsPrefix(string prefix)
    {
        return Cookies.ContainsKey(prefix);
    }

    public override ValueProviderResult GetValue(string key)
    {
        return Cookies.TryGetValue(key, out var value) ? new ValueProviderResult(value) : ValueProviderResult.None;
    }
}