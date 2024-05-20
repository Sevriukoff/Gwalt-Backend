using Microsoft.AspNetCore.Mvc.ModelBinding;
using Sevriukoff.Gwalt.Application.Helpers;
using Sevriukoff.Gwalt.WebApi.Controllers;

namespace Sevriukoff.Gwalt.WebApi.Common.Attributes;

public class JwtValueProviderFactory : IValueProviderFactory
{
    private readonly JwtHelper _jwtHelper;

    public JwtValueProviderFactory(JwtHelper jwtHelper)
    {
        _jwtHelper = jwtHelper;
    }
    
    public Task CreateValueProviderAsync(ValueProviderFactoryContext context)
    {
        var result = context.ActionContext.HttpContext.Request.Cookies.TryGetValue("jwt-access", out var token);

        if (result)
        {
            var claims = _jwtHelper.GetClaims(token!);
            
            foreach (var claim in claims)
            {
                context.ValueProviders.Add(new DefaultAttributeValueProvider(ExtensionBindingSources.Jwt, claim.Type, claim.Value));
            }
        }

        return Task.CompletedTask;
    }
}