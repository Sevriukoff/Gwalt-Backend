using Sevriukoff.Gwalt.Application.Enums;

namespace Sevriukoff.Gwalt.WebApi.QueryParameters;

public class LikeQueryParameters : BaseQueryParameters
{
    public LikeableType Type { get; set; }
}