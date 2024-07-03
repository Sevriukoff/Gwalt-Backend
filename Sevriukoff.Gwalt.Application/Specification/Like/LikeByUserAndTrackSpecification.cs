namespace Sevriukoff.Gwalt.Application.Specification.Like;

public class LikeByUserAndTrackSpecification : Specification<Infrastructure.Entities.Like>
{
    public LikeByUserAndTrackSpecification(int userId, int trackId)
    {
        SetFilterCondition(l => l.UserId == userId && l.TrackId == trackId);
    }
}