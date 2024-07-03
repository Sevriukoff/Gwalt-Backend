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
        var userId = like.UserId;

        var spec = new LikeByUserAndCommentSpecification(userId, commentId);
        var likeEntity = await LikeRepository.GetAsync(spec);

        return likeEntity != null;
    }

    public override async Task<LikeModel?> GetAsync(int trackId, int userId)
    {
        throw new NotImplementedException();
    }

    public override async Task<IEnumerable<LikeModel>> GetAllByUserIdAsync(int userId, int pageNumber = 1, int pageSize = 10)
    {
        var spec = new LikesByUserOnCommentsSpecification(userId);
        var likes = await LikeRepository.GetAllAsync(pageNumber, pageSize, spec);
        
        return likes.Select(x => new LikeModel
        {
            Id = x.Id,
            Likeable = new TrackModel
            {
                Id = x.CommentId!.Value
            },
            ReleaseDate = x.ReleaseDate
        });
    }
}