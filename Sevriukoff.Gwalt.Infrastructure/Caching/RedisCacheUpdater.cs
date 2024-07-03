using Microsoft.EntityFrameworkCore;
using Sevriukoff.Gwalt.Infrastructure.Interfaces;

namespace Sevriukoff.Gwalt.Infrastructure.Caching;

public class RedisCacheUpdater : ICacheUpdater
{
    private readonly ITrackRepository _trackRepository;
    private readonly IAlbumRepository _albumRepository;
    private readonly IUserRepository _userRepository;
    
    private readonly ListenCacheClient _listenCacheClient;
    private readonly LikeCacheClient _likeCacheClient;
    private readonly FollowerCacheClient _followerCacheClient;

    public RedisCacheUpdater(ITrackRepository trackRepository,
        IUserRepository userRepository,
        ListenCacheClient listenCacheClient,
        FollowerCacheClient followerCacheClient,
        IAlbumRepository albumRepository,
        LikeCacheClient likeCacheClient)
    {
        _trackRepository = trackRepository;
        _listenCacheClient = listenCacheClient;
        _userRepository = userRepository;
        _followerCacheClient = followerCacheClient;
        _albumRepository = albumRepository;
        _likeCacheClient = likeCacheClient;
    }

    public async Task UpdateListensCountsAsync()
    {
        var trackListensCounts = await _listenCacheClient.GetTrackListensCountsAsync();
        var albumListensCounts = await _listenCacheClient.GetAlbumListensCountsAsync();
        var userListensCounts = await _listenCacheClient.GetUserListensCountsAsync();
        
        foreach (var (trackId, listensCount) in trackListensCounts)
           await _trackRepository.IncrementListensAsync(trackId, listensCount);
        
        foreach (var (albumId, listensCount) in albumListensCounts)
            await _albumRepository.IncrementListensAsync(albumId, listensCount);
        
        foreach (var (userId, listensCount) in userListensCounts)
            await _userRepository.IncrementListensAsync(userId, listensCount);
        
        await _listenCacheClient.ClearTrackListensCountsAsync();
        await _listenCacheClient.ClearAlbumListensCountsAsync();
        await _listenCacheClient.ClearUserListensCountsAsync();
    }
    
    public async Task UpdateLikesCountsAsync()
    {
        var trackLikesCounts = await _likeCacheClient.GetTrackLikesCountsAsync();
        var albumLikesCounts = await _likeCacheClient.GetAlbumLikesCountsAsync();
        var userLikesCounts = await _likeCacheClient.GetUserLikesCountsAsync();
        
        foreach (var (trackId, likesCount) in trackLikesCounts)
            await _trackRepository.IncrementLikesAsync(trackId, likesCount);
        
        foreach (var (albumId, likesCount) in albumLikesCounts)
            await _albumRepository.IncrementLikesAsync(albumId, likesCount);
        
        foreach (var (userId, likesCount) in userLikesCounts)
            await _userRepository.IncrementLikesAsync(userId, likesCount);
        
        await _likeCacheClient.ClearTrackLikesCountsAsync();
        await _likeCacheClient.ClearAlbumLikesCountsAsync();
        await _likeCacheClient.ClearUserLikesCountsAsync();
    }

    public async Task UpdateFollowerCountsAsync()
    {
        var followerCounts = await _followerCacheClient.GetFollowersCountAsync();
        var followingsCounts = await _followerCacheClient.GetFollowingsCountAsync();

        foreach (var (userId, followersCount) in followerCounts)
            await _userRepository.IncrementFollowersAsync(userId, followersCount);
        
        foreach (var (userId, followingsCount) in followingsCounts)
            await _userRepository.IncrementFollowingsAsync(userId, followingsCount);

        await _followerCacheClient.ClearFollowersCountAsync();
        await _followerCacheClient.ClearFollowingsCountAsync();
    }
}