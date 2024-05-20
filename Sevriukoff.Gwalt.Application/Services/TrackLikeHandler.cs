using Sevriukoff.Gwalt.Application.Enums;
using Sevriukoff.Gwalt.Application.Specification.Like;
using Sevriukoff.Gwalt.Infrastructure.Entities;
using Sevriukoff.Gwalt.Infrastructure.Interfaces;

namespace Sevriukoff.Gwalt.Application.Services;

public class TrackLikeHandler : LikeHandlerBase
{
    private readonly ITrackRepository _trackRepository;
    public override LikeableType LikeableType => LikeableType.Track;
    
    public TrackLikeHandler(ILikeRepository likeRepository, ITrackRepository trackRepository) : base(likeRepository)
    {
        _trackRepository = trackRepository;
    }
    
    protected override Like CreateLike(int likeableId, int userId)
    {
        return new Like
        {
            TrackId = likeableId,
            LikeById = userId,
            ReleaseDate = DateTime.UtcNow
        };
    }

    protected override async Task IncrementLikeCountAsync(int likeableId)
    {
        var track = await _trackRepository.GetByIdAsync(likeableId);
        track.LikeCount++;
        await _trackRepository.UpdateAsync(track);
    }

    protected override async Task DecrementLikeCountAsync(int likeableId)
    {
        var track = await _trackRepository.GetByIdAsync(likeableId);
        track.LikeCount--;
        await _trackRepository.UpdateAsync(track);
    }

    protected override async Task<bool> IsExists(Like like)
    {
        var trackId = like.TrackId.Value;
        var userId = like.LikeById;

        var spec = new LikeOnTrackExistSpecification(userId, trackId);
        var likeEntity = await _likeRepository.GetAsync(spec);

        return likeEntity != null;
    }
}