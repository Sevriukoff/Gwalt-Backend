namespace Sevriukoff.Gwalt.Application.Specification.Album;

public class AlbumsByGenresSpecification : Specification<Infrastructure.Entities.Album>
{
    public AlbumsByGenresSpecification(params string[]? genres)
    {
        if (genres == null || genres.Length == 0)
            return;
        
        if (genres.Length == 1)
            SetFilterCondition(x => x.Genre.Name == genres[0]);
        else
            SetFilterCondition(x => genres.Contains(x.Genre.Name));
    }
}