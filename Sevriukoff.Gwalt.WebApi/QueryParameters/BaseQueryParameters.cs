namespace Sevriukoff.Gwalt.WebApi.QueryParameters;

public abstract class BaseQueryParameters
{
    public string? Includes { get; set; }
    public string? OrderBy { get; set; }
}