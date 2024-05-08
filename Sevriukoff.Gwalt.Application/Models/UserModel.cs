namespace Sevriukoff.Gwalt.Application.Models;

public class UserModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public DateTime RegistrationDate { get; set; }
    public string AvatarUrl { get; set; }
    public string BackgroundUrl { get; set; }
    public string ShortDescription { get; set; }
    public string Description { get; set; }

    public List<AlbumModel> Albums { get; set; }
    
    public GenreModel[] GetGenres()
    {
        return Albums.SelectMany(album => album.Tracks.SelectMany(track => track.Genres))
            .Distinct()
            .ToArray();
    }
    
    public int GetLikesCount()
    {
        return Albums.Sum
        (
            x => x.Tracks.Sum(x => x.LikesCount)
        );
    }
    
    public int GetListensCount()
    {
        return Albums.Sum
        (
            x => x.Tracks.Sum(x => x.ListensCount)
        );
    }
    
    public int GetSharesCount()
    {
        return Albums.Sum
        (
            x => x.Tracks.Sum(x => x.SharesCount)
        );
    }
}