using Sevriukoff.Gwalt.Application.Enums;
using Sevriukoff.Gwalt.Application.Models;
using Sevriukoff.Gwalt.Application.Services;
using Sevriukoff.Gwalt.Application.Specification.Like;
using Sevriukoff.Gwalt.Infrastructure.Entities;
using Sevriukoff.Gwalt.Infrastructure.Interfaces;

namespace Sevriukoff.Gwalt.Application.Handlers;

public class CommentLikeHandler : LikeHandlerBase
{
    public override LikeableType LikeableType => LikeableType.Comment;
    
    public CommentLikeHandler(ILikeRepository likeRepository) : base(likeRepository)
    {
    }
    protected override Like CreateLike(int likeableId, int userId)
    {
        throw new NotImplementedException();
    }

    protected override async Task IncrementLikeCountAsync(int likeableId)
    {
        throw new NotImplementedException();
    }

    protected override async Task DecrementLikeCountAsync(int likeableId)
    {
        throw new NotImplementedException();
    }

    protected override async Task<bool> IsExists(Like like)
    {
        var commentId = like.CommentId.Value;
        var userId = like.LikeById;

        var spec = new LikeOnCommentSpecification(userId, commentId);
        var likeEntity = await _likeRepository.GetAsync(spec);

        return likeEntity != null;
    }

    public override async Task<LikeModel> GetLikeAsync(int likeableId, int userId)
    {
        throw new NotImplementedException();
    }
}