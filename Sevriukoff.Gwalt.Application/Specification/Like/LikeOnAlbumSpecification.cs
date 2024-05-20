namespace Sevriukoff.Gwalt.Application.Specification.Like;

public class LikeOnAlbumSpecification : Specification<Infrastructure.Entities.Like>
{
    public LikeOnAlbumSpecification(int userId, int albumId)
    {
        SetFilterCondition(l => l.LikeById == userId && l.AlbumId == albumId);
    }
}