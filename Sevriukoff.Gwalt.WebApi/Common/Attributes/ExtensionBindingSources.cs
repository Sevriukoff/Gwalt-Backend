using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Sevriukoff.Gwalt.WebApi.Common.Attributes;

public static class ExtensionBindingSources
{
    public static readonly BindingSource Cookie = new BindingSource(
        "Cookie",
        "Cookie",
        isGreedy: false,
        isFromRequest: true);
    
    public static readonly BindingSource Jwt = new BindingSource(
        "JWT",
        "JWT",
        isGreedy: false,
        isFromRequest: true
    );
}