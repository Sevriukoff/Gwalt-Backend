using Sevriukoff.Gwalt.Application.Enums;
using Sevriukoff.Gwalt.Application.Helpers;
using Sevriukoff.Gwalt.Application.Models;
using Sevriukoff.Gwalt.Application.Specification;
using Sevriukoff.Gwalt.Application.Specification.Listen;
using Sevriukoff.Gwalt.Infrastructure.Entities;
using Sevriukoff.Gwalt.Infrastructure.Interfaces;

namespace Sevriukoff.Gwalt.Application.Handlers;

public class TrackListenHandler : ListenHandlerBase
{
    public override ListenableType ListenableType => ListenableType.Track;
    private readonly ITrackRepository _trackRepository;
    private readonly IAlbumRepository _albumRepository;
    private readonly IListenCacheService _listenCacheService;
    
    public TrackListenHandler(IListenRepository listenRepository, ITrackRepository trackRepository,
        IAlbumRepository albumRepository, IListenCacheService listenCacheService) : base(listenRepository)
    {
        _trackRepository = trackRepository;
        _albumRepository = albumRepository;
        _listenCacheService = listenCacheService;
    }
    
    protected override int EvaluateQuality(ListenMetadata metadata, UserModel userModel)
    {
        int score = 0;
        
        var listeningPercentage = metadata.ActiveListeningTime / metadata.TotalDuration * 100;
        score += (int)(listeningPercentage * 5);
        
        var endTimePercentage = metadata.EndTime / metadata.TotalDuration * 100;
        
        score += (int)(endTimePercentage * 3);
        score -= metadata.SeekCount * 10;
        score -= metadata.PauseCount * 5;
        score += metadata.Volume;

        if (userModel.Id > 0)
        {
            score += 50;
        }

        score = Math.Max(0, Math.Min(score, 1000));

        return score;
    }

    protected override Listen CreateListen(ListenModel listenModel, int quality)
    {
        var listen = new Listen
        {
            TrackId = listenModel.Listenable.Id,
            ReleaseDate = DateTime.UtcNow,
            Quality = quality,
            ActiveListeningTime = listenModel.Metadata.ActiveListeningTime,
            EndTime = listenModel.Metadata.EndTime,
            TotalDuration = listenModel.Metadata.TotalDuration,
            SeekCount = listenModel.Metadata.SeekCount,
            PauseCount = listenModel.Metadata.PauseCount,
            Volume = listenModel.Metadata.Volume,
        };

        if (listen.UserId > 0)
            listen.UserId = listenModel.User.Id;
        else
            listen.SessionId = listenModel.SessionId;

        return listen;
    }

    protected override async Task IncrementListenCountAsync(int trackId)
    {
        var track = await _trackRepository.GetByIdAsync(trackId);
        var album = await _albumRepository.GetByIdAsync(track.AlbumId);
        
        await _listenCacheService.IncrementTrackPlayCountAsync(trackId);
        await _listenCacheService.IncrementAlbumPlayCountAsync(track.AlbumId);
        
        track.PlayCount++;
        album.PlayCount++;
        
        await _albumRepository.UpdateAsync(album);
    }

    public override async Task<ListenModel?> GetListenAsync(int trackId, int userId)
    {
        var spec = new ListenOnTrackSpecification(userId, trackId);
        var listen = await ListenRepository.GetAsync(spec);
        
        if (listen == null)
            return null;
        
        return new ListenModel
        {
            Id = listen.Id,
            Listenable = new TrackModel
            {
                Id = trackId
            },
            ReleaseDate = listen.ReleaseDate
        };
    }
}