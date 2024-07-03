namespace Sevriukoff.Gwalt.Infrastructure.Caching;

public interface ICacheUpdater
{
    Task UpdateListensCountsAsync();
    Task UpdateLikesCountsAsync();
    Task UpdateFollowerCountsAsync();
}