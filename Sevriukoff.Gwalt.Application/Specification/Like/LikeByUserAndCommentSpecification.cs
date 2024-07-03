namespace Sevriukoff.Gwalt.Application.Specification.Like;

public class LikeByUserAndCommentSpecification : Specification<Infrastructure.Entities.Like>
{
    public LikeByUserAndCommentSpecification(int userId, int commentId)
    {
        SetFilterCondition(l => l.UserId == userId && l.CommentId == commentId);
    }
}