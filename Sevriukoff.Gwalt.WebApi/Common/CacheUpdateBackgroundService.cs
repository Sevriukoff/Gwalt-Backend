using Sevriukoff.Gwalt.Infrastructure.Caching;

namespace Sevriukoff.Gwalt.WebApi.Common;

public class CacheUpdateBackgroundService : BackgroundService
{
    private readonly ICacheUpdater _cacheUpdater;
    private readonly ILogger<CacheUpdateBackgroundService> _logger;
    private readonly TimeSpan _listensCountUpdateInterval = TimeSpan.FromMinutes(1); // Интервал обновления прослушиваний треков
    private readonly TimeSpan _likesCountUpdateInterval = TimeSpan.FromMinutes(1); // Интервал обновления лайков треков
    private readonly TimeSpan _followerCountUpdateInterval = TimeSpan.FromMinutes(1); // Интервал обновления фоллоу

    public CacheUpdateBackgroundService(ICacheUpdater cacheUpdater, ILogger<CacheUpdateBackgroundService> logger)
    {
        _cacheUpdater = cacheUpdater;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var listensCountTask = UpdateTrackPlayCountsAsync(stoppingToken);
        var likesCountTask = UpdateLikesCountsAsync(stoppingToken);
        var followerCountTask = UpdateFollowerCountsAsync(stoppingToken);

        await Task.WhenAll(listensCountTask, likesCountTask, followerCountTask);
    }

    private async Task UpdateTrackPlayCountsAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                _logger.LogInformation("Updating track play counts from cache...");
                await _cacheUpdater.UpdateListensCountsAsync();
                _logger.LogInformation("Track play counts update complete.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating track play counts from cache.");
            }

            await Task.Delay(_listensCountUpdateInterval, stoppingToken);
        }
    }
    
    private async Task UpdateLikesCountsAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                _logger.LogInformation("Updating likes counts from cache...");
                await _cacheUpdater.UpdateLikesCountsAsync();
                _logger.LogInformation("Likes counts update complete.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating likes counts from cache.");
            }
            
            await Task.Delay(_likesCountUpdateInterval, stoppingToken);
        }
    }

    private async Task UpdateFollowerCountsAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                _logger.LogInformation("Updating follower counts from cache...");
                await _cacheUpdater.UpdateFollowerCountsAsync();
                _logger.LogInformation("Follower counts update complete.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating follower counts from cache.");
            }

            await Task.Delay(_followerCountUpdateInterval, stoppingToken);
        }
    }
}