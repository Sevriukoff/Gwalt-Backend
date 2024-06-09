namespace Sevriukoff.Gwalt.Application.Specification.Like;

public class LikeOnAlbumSpecification : Specification<Infrastructure.Entities.Like>
{
    public LikeOnAlbumSpecification(int userId, int albumId)
    {
        SetFilterCondition(l => l.UserId == userId && l.AlbumId == albumId);
    }
}