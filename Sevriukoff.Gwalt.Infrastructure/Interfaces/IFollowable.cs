namespace Sevriukoff.Gwalt.Infrastructure.Interfaces;

public interface IHasFollowers
{
    int FollowersCount { get; set; }
    int FollowingCount { get; set; }
}