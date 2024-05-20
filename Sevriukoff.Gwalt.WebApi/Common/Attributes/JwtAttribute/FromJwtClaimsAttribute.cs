using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Sevriukoff.Gwalt.WebApi.Common.Attributes;

[AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
public class FromJwtClaimsAttribute : Attribute, IBindingSourceMetadata, IModelNameProvider
{
    public FromJwtClaimsAttribute(string claimName)
    {
        Name = claimName;
    }
    
    public BindingSource BindingSource => ExtensionBindingSources.Jwt;

    public string Name { get; set; }
}