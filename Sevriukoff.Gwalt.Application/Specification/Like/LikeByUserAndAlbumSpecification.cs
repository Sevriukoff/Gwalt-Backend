namespace Sevriukoff.Gwalt.Application.Specification.Like;

public class LikeByUserAndAlbumSpecification : Specification<Infrastructure.Entities.Like>
{
    public LikeByUserAndAlbumSpecification(int userId, int albumId)
    {
        SetFilterCondition(l => l.UserId == userId && l.AlbumId == albumId);
    }
}