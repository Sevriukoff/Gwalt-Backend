namespace Sevriukoff.Gwalt.Application.Specification.Like;

public class LikesByUserOnAlbumsSpecification : Specification<Infrastructure.Entities.Like>
{
    public LikesByUserOnAlbumsSpecification(int? userId)
    {
        if (userId == null)
            return;
        
        SetFilterCondition(x => x.UserId == userId && x.AlbumId != null);
    }
}