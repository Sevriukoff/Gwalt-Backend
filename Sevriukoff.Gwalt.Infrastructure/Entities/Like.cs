using Sevriukoff.Gwalt.Infrastructure.Base;

namespace Sevriukoff.Gwalt.Infrastructure.Entities;

public class Like : GenMetric
{
    public int? UserProfileId { get; set; }
    public User UserProfile { get; set; }
}