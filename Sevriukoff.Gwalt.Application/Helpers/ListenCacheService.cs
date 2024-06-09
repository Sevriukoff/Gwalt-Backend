using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;

namespace Sevriukoff.Gwalt.Application.Helpers;

public class ListenCacheService : IListenCacheService
{
    private readonly IDistributedCache _distributedCache;

    public ListenCacheService(IDistributedCache distributedCache)
    {
        _distributedCache = distributedCache;
    }

    private async Task IncrementCountAsync(string key)
    {
        var countString = await _distributedCache.GetStringAsync(key);
        int count = countString != null ? int.Parse(countString) : 0;
        count++;
        await _distributedCache.SetStringAsync(key, count.ToString());
    }

    public async Task IncrementTrackPlayCountAsync(int trackId)
    {
        string key = $"track:{trackId}:playcount";
        await IncrementCountAsync(key);
    }

    public async Task IncrementAlbumPlayCountAsync(int albumId)
    {
        string key = $"album:{albumId}:playcount";
        await IncrementCountAsync(key);
    }

    private async Task<Dictionary<int, int>> GetPlayCountsAsync(string prefix)
    {
        var result = new Dictionary<int, int>();
        var keys = await GetKeysAsync(prefix);
        foreach (var key in keys)
        {
            var idString = key.Replace(prefix, "");
            if (int.TryParse(idString, out var id))
            {
                var countString = await _distributedCache.GetStringAsync(key);
                int count = countString != null ? int.Parse(countString) : 0;
                result[id] = count;
            }
        }
        return result;
    }

    public Task<Dictionary<int, int>> GetTrackPlayCountsAsync()
    {
        return GetPlayCountsAsync("track:");
    }

    public Task<Dictionary<int, int>> GetAlbumPlayCountsAsync()
    {
        return GetPlayCountsAsync("album:");
    }

    private async Task<IEnumerable<string>> GetKeysAsync(string prefix)
    {
        // Redis key pattern matching is not supported directly by IDistributedCache, so we need to use a workaround
        // Assume we have a separate list storing all keys for simplicity
        string keyList = await _distributedCache.GetStringAsync("keys");
        var keys = keyList?.Split(',').Where(k => k.StartsWith(prefix)).ToList() ?? new List<string>();
        return keys;
    }

    public async Task ClearTrackPlayCountsAsync()
    {
        var keys = await GetKeysAsync("track:");
        foreach (var key in keys)
        {
            await _distributedCache.RemoveAsync(key);
        }
    }

    public async Task ClearAlbumPlayCountsAsync()
    {
        var keys = await GetKeysAsync("album:");
        foreach (var key in keys)
        {
            await _distributedCache.RemoveAsync(key);
        }
    }
}