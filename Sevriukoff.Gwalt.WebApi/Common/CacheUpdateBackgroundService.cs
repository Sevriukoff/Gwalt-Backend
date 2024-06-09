using Sevriukoff.Gwalt.Application.Interfaces;

namespace Sevriukoff.Gwalt.WebApi.Common;

public class CacheUpdateBackgroundService : BackgroundService
{
    private readonly ITrackService _trackService;
    private readonly ILogger<CacheUpdateBackgroundService> _logger;
    private readonly TimeSpan _interval = TimeSpan.FromMinutes(5); // Интервал обновления

    public CacheUpdateBackgroundService(ITrackService trackService, ILogger<CacheUpdateBackgroundService> logger)
    {
        _trackService = trackService;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                _logger.LogInformation("Updating database from cache...");
                await _trackService.UpdateDatabaseFromCacheAsync();
                _logger.LogInformation("Database update complete.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating database from cache.");
            }

            await Task.Delay(_interval, stoppingToken);
        }
    }
}