using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Sevriukoff.Gwalt.WebApi.Common.Attributes;

public class DefaultAttributeValueProvider : BindingSourceValueProvider
{
    private readonly string _key;
    private readonly string _value;

    public DefaultAttributeValueProvider(BindingSource bindingSource, string key, string value) : base(bindingSource)
    {
        _key = key;
        _value = value;
    }

    public override bool ContainsPrefix(string prefix)
    {
        return string.Equals(prefix, _key, StringComparison.OrdinalIgnoreCase);
    }

    public override ValueProviderResult GetValue(string key)
    {
        return string.Equals(key, _key, StringComparison.OrdinalIgnoreCase)
            ? new ValueProviderResult(_value)
            : ValueProviderResult.None;
    }
}