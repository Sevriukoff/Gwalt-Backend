using AutoMapper;
using Sevriukoff.Gwalt.Application.Enums;
using Sevriukoff.Gwalt.Application.Models;
using Sevriukoff.Gwalt.Application.Specification;
using Sevriukoff.Gwalt.Application.Specification.Listen;
using Sevriukoff.Gwalt.Infrastructure.Caching;
using Sevriukoff.Gwalt.Infrastructure.Entities;
using Sevriukoff.Gwalt.Infrastructure.Interfaces;

namespace Sevriukoff.Gwalt.Application.Handlers;

public class TrackListenHandler : ListenHandlerBase
{
    public override ListenableType ListenableType => ListenableType.Track;
    
    private readonly ListenCacheClient _listenCacheClient;
    private readonly ITrackRepository _trackRepository;
    private readonly IMapper _mapper;

    public TrackListenHandler(IListenRepository listenRepository, ListenCacheClient listenCacheClient, IMapper mapper, ITrackRepository trackRepository) : base(listenRepository)
    {
        _listenCacheClient = listenCacheClient;
        _trackRepository = trackRepository;
        _mapper = mapper;
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
            score += 150;
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
        var (albumId, authorsIds) = await _trackRepository.GetAuthorsIdsByTrackIdAsync(trackId);
        
        await _listenCacheClient.IncrementTrackListensAsync(trackId);
        await _listenCacheClient.IncrementAlbumListensAsync(albumId);
        
        foreach (var authorId in authorsIds)
        {
            await _listenCacheClient.IncrementUserListensAsync(authorId);
        }
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

    public override async Task<IEnumerable<ListenModel>> GetListensByUserIdAsync(int userId, string[]? includes, int pageNumber, int pageSize)
    {
        var spec = new ListensByUserSpecification(userId);
        var includeSpec = new IncludingSpecification<Listen>(includes);
        var listens = await ListenRepository.GetAllAsync(pageNumber, pageSize, specification:spec.And(includeSpec));
        
        var listenModels = listens.Select(x => new ListenModel
        {
            Id = x.Id,
            Listenable = x.Track != null ? _mapper.Map<TrackModel>(x.Track) : new TrackModel {Id = x.TrackId!.Value},
            ReleaseDate = x.ReleaseDate
        });
        
        return listenModels;
    }

    public override async Task<IEnumerable<ListenModel>> GetListensBySessionIdAsync(string sessionId, string[]? includes, int pageNumber, int pageSize)
    {
        var spec = new ListensBySessionIdSpecification(sessionId);
        var includeSpec = new IncludingSpecification<Listen>(includes);
        var listens = await ListenRepository.GetAllAsync(pageNumber, pageSize, specification:spec.And(includeSpec));
        
        var listenModels = listens.Select(x => new ListenModel
        {
            Id = x.Id,
            Listenable = x.Track != null ? _mapper.Map<TrackModel>(x.Track) : new TrackModel {Id = x.TrackId!.Value},
            ReleaseDate = x.ReleaseDate,
            Metadata = new ListenMetadata
            {
                Quality = x.Quality,
                ActiveListeningTime = x.ActiveListeningTime,
                EndTime = x.EndTime,
                TotalDuration = x.TotalDuration,
                SeekCount = x.SeekCount,
                PauseCount = x.PauseCount,
                Volume = x.Volume
            }
        });
        
        return listenModels;
    }
}