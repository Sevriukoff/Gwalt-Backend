namespace Sevriukoff.Gwalt.Application.Specification.Like;

public class LikeOnCommentSpecification : Specification<Infrastructure.Entities.Like>
{
    public LikeOnCommentSpecification(int userId, int commentId)
    {
        SetFilterCondition(l => l.UserId == userId && l.CommentId == commentId);
    }
}