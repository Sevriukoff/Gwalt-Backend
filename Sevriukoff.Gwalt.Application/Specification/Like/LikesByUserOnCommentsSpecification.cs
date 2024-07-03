namespace Sevriukoff.Gwalt.Application.Specification.Like;

public class LikesByUserOnCommentsSpecification : Specification<Infrastructure.Entities.Like>
{
    public LikesByUserOnCommentsSpecification(int? userId)
    {
        if (userId == null)
            return;
        
        SetFilterCondition(x => x.UserId == userId && x.CommentId != null);
    }
}