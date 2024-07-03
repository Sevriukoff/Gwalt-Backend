namespace Sevriukoff.Gwalt.WebApi.QueryParameters;

public class BaseQueryParameters
{
    public string? Includes { get; set; }
    public string? OrderBy { get; set; }
    
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}