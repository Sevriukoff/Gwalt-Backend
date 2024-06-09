namespace Sevriukoff.Gwalt.Application.Helpers;

public interface IListenCacheService
{
    Task IncrementTrackPlayCountAsync(int trackId);
    Task IncrementAlbumPlayCountAsync(int albumId);
    Task<Dictionary<int, int>> GetTrackPlayCountsAsync();
    Task<Dictionary<int, int>> GetAlbumPlayCountsAsync();
    Task ClearTrackPlayCountsAsync();
    Task ClearAlbumPlayCountsAsync();
}