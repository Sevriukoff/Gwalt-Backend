namespace Sevriukoff.Gwalt.Application.Specification.Like;

public class LikesByUserOnTracksSpecification : Specification<Infrastructure.Entities.Like>
{
    public LikesByUserOnTracksSpecification(int? userId)
    {
        if (userId == null)
            return;
        
        SetFilterCondition(x => x.UserId == userId && x.TrackId != null);
    }
}