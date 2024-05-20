using Sevriukoff.Gwalt.Application.Enums;
using Sevriukoff.Gwalt.Application.Interfaces;
using Sevriukoff.Gwalt.Infrastructure.Entities;
using Sevriukoff.Gwalt.Infrastructure.Interfaces;

namespace Sevriukoff.Gwalt.Application.Services;

public class LikeService : ILikeService
{
    private readonly ILikeRepository _likeRepository;
    private readonly IDictionary<LikeableType, ILikeHandler> _handlers;
    
    public LikeService(IEnumerable<ILikeHandler> handlers, ILikeRepository likeRepository)
    {
        _likeRepository = likeRepository;
        _handlers = handlers.ToDictionary(x => x.LikeableType, x => x);
    }
    
    public async Task<int> AddAsync(LikeableType likeableType, int likeableId, int userId)
    {
        if (_handlers.TryGetValue(likeableType, out var handler))
        {
            return await handler.AddLikeAsync(likeableId, userId);
        }
        
        throw new Exception("Handler not found");
    }

    public async Task DeleteAsync(int likeId)
    {
        var like = await _likeRepository.GetByIdAsync(likeId);
        var (likeableType, likeableId) = GetLikeableType(like);
        
        if (_handlers.TryGetValue(likeableType, out var handler))
        {
            await handler.DeleteLikeAsync(likeableId, likeId);
        }
    }

    private (LikeableType likeableType, int likeableId) GetLikeableType(Like like)
    {
        if (like.TrackId.HasValue)
            return (LikeableType.Track, like.TrackId.Value);
        if (like.AlbumId.HasValue)
            return (LikeableType.Album, like.AlbumId.Value);
        
        return like.UserProfileId.HasValue
            ? (LikeableType.Profile, like.UserProfileId.Value)
            : (LikeableType.Comment, like.CommentId.Value);
    }
}