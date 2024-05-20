using Sevriukoff.Gwalt.Infrastructure.Base;

namespace Sevriukoff.Gwalt.Infrastructure.Entities;

public class Album : BaseEntity
{
    public string Title { get; set; }
    public string ImageUrl { get; set; }
    public bool IsSingle { get; set; }
    public DateTime ReleaseDate { get; set; }
    
    public int LikeCount { get; set; }
    public int ShareCount { get; set; }
    public int PlayCount { get; set; }

    #region NavigationProperties

    public ICollection<User> Authors { get; set; }
    public ICollection<Track> Tracks { get; set; }
    
    public ICollection<Like> Likes { get; set; }
    public ICollection<Listen> Listens { get; set; }
    public ICollection<Share> Shares { get; set; }

    #endregion
}