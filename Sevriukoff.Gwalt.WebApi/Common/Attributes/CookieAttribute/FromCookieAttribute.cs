using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Sevriukoff.Gwalt.WebApi.Common.Attributes;

[AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
public class FromCookieAttribute : Attribute, IBindingSourceMetadata, IModelNameProvider
{
    public FromCookieAttribute(string cookie)
    {
        Name = cookie;
    }
    
    public BindingSource BindingSource => ExtensionBindingSources.Cookie;

    public string Name { get; set; }
}