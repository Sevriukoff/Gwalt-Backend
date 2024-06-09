namespace Sevriukoff.Gwalt.Application.Specification.Listen;

public class ListenOnTrackSpecification : Specification<Infrastructure.Entities.Listen>
{
    public ListenOnTrackSpecification(int userId, int trackId)
    {
        SetFilterCondition(l => l.UserId == userId && l.TrackId == trackId);
    }
}