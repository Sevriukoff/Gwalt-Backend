using Sevriukoff.Gwalt.Infrastructure.Entities;

namespace Sevriukoff.Gwalt.Infrastructure.Base;

public abstract class Metric : BaseEntity
{
    public DateTime ReleaseDate { get; set; }
}