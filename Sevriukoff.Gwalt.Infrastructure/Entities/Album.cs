using Sevriukoff.Gwalt.Infrastructure.Base;
using Sevriukoff.Gwalt.Infrastructure.Interfaces;

namespace Sevriukoff.Gwalt.Infrastructure.Entities;

public class Album : BaseEntity, IHasListens, IHasLikes
{
    public string Title { get; set; }
    public string TsvectorTitle { get; set; }
    public string ImageUrl { get; set; }
    public bool IsSingle { get; set; }
    public DateTime ReleaseDate { get; set; }
    public int GenreId { get; set; }
    
    public int TracksCount { get; set; }
    public TimeSpan Duration { get; set; }
    public int LikesCount { get; set; }
    public int SharesCount { get; set; }
    public int ListensCount { get; set; }

    #region NavigationProperties
    
    public Genre Genre { get; set; }
    public ICollection<User> Authors { get; set; }
    public ICollection<Track> Tracks { get; set; }
    public ICollection<Like> Likes { get; set; }
    public ICollection<Listen> Listens { get; set; }
    public ICollection<Share> Shares { get; set; }

    #endregion
}