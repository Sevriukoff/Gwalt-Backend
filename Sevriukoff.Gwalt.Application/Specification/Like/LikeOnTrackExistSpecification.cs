namespace Sevriukoff.Gwalt.Application.Specification.Like;

public class LikeOnTrackExistSpecification : Specification<Infrastructure.Entities.Like>
{
    public LikeOnTrackExistSpecification(int userId, int trackId)
    {
        SetFilterCondition(l => l.UserId == userId && l.TrackId == trackId);
    }
}