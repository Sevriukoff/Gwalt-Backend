using AutoMapper;
using Sevriukoff.Gwalt.Application.Enums;
using Sevriukoff.Gwalt.Application.Models;
using Sevriukoff.Gwalt.Application.Specification;
using Sevriukoff.Gwalt.Application.Specification.Like;
using Sevriukoff.Gwalt.Infrastructure.Caching;
using Sevriukoff.Gwalt.Infrastructure.Entities;
using Sevriukoff.Gwalt.Infrastructure.Interfaces;

namespace Sevriukoff.Gwalt.Application.Handlers;

public class TrackLikeHandler : LikeHandlerBase
{
    private readonly ITrackRepository _trackRepository;
    private readonly LikeCacheClient _likeCacheClient;
    private readonly IMapper _mapper;
    public override LikeableType LikeableType => LikeableType.Track;
    
    public TrackLikeHandler(ILikeRepository likeRepository, ITrackRepository trackRepository, IMapper mapper, LikeCacheClient likeCacheClient) : base(likeRepository)
    {
        _trackRepository = trackRepository;
        _mapper = mapper;
        _likeCacheClient = likeCacheClient;
    }
    
    protected override Like CreateLike(int likeableId, int userId)
    {
        return new Like
        {
            TrackId = likeableId,
            UserId = userId,
            ReleaseDate = DateTime.UtcNow
        };
    }

    protected override async Task IncrementLikeCountAsync(int likeableId)
    {
        var (albumId, authorsIds) = await _trackRepository.GetAuthorsIdsByTrackIdAsync(likeableId);
        
        await _likeCacheClient.IncrementTrackLikesAsync(likeableId);
        await _likeCacheClient.IncrementAlbumLikesAsync(albumId);
        
        foreach (var authorId in authorsIds)
        {
            await _likeCacheClient.IncrementUserLikeAsync(authorId);
        }
        
        /*
        var track = await _trackRepository.GetByIdAsync(likeableId);
        track.LikesCount++;
        await _trackRepository.UpdateAsync(track);
        */
    }

    protected override async Task DecrementLikeCountAsync(int likeableId)
    {
        var (albumId, authorsIds) = await _trackRepository.GetAuthorsIdsByTrackIdAsync(likeableId);
        
        await _likeCacheClient.DecrementTrackLikesAsync(likeableId);
        await _likeCacheClient.DecrementAlbumLikesAsync(albumId);
        
        foreach (var authorId in authorsIds)
        {
            await _likeCacheClient.DecrementUserLikeAsync(authorId);
        }
        
        /*
        var track = await _trackRepository.GetByIdAsync(likeableId);
        track.LikesCount--;
        await _trackRepository.UpdateAsync(track);
        */
    }

    protected override async Task<bool> IsExists(Like like)
    {
        var trackId = like.TrackId.Value;
        var userId = like.UserId;

        var spec = new LikeByUserAndTrackSpecification(userId, trackId);
        var likeEntity = await LikeRepository.GetAsync(spec);

        return likeEntity != null;
    }

    public override async Task<LikeModel?> GetAsync(int trackId, int userId)
    {
        var spec = new LikeByUserAndTrackSpecification(userId, trackId);
        var like = await LikeRepository.GetAsync(spec);

        if (like == null)
            return null;
        
        return new LikeModel
        {
            Id = like.Id,
            Likeable = new TrackModel
            {
                Id = trackId
            },
            ReleaseDate = like.ReleaseDate
        };
    }
    
    public override async Task<IEnumerable<LikeModel>> GetAllByUserIdAsync(int userId, int pageNumber = 1, int pageSize = 10)
    {
        var spec = new LikesByUserOnTracksSpecification(userId);
        var includeSpec = new IncludingSpecification<Like>("Track.Album.Authors");
        
        var likes = await LikeRepository.GetAllAsync(pageNumber, pageSize, spec.And(includeSpec));
        
        return likes.Select(x => new LikeModel
        {
            Id = x.Id,
            Likeable = _mapper.Map<TrackModel>(x.Track),
            ReleaseDate = x.ReleaseDate
        });
    }
}