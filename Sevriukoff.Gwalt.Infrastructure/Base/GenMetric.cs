using Sevriukoff.Gwalt.Infrastructure.Entities;

namespace Sevriukoff.Gwalt.Infrastructure.Base;

public abstract class GenMetric : Metric
{
    public int? CommentId { get; set; }
    public Comment Comment { get; set; }
}