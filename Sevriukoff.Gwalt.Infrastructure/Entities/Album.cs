using Sevriukoff.Gwalt.Infrastructure.Base;

namespace Sevriukoff.Gwalt.Infrastructure.Entities;

public class Album : BaseEntity
{
    public string Title { get; set; }
    public string ImageUrl { get; set; }
    public bool IsSingle { get; set; }
    public DateTime ReleaseDate { get; set; }

    #region NavigationProperties

    public ICollection<User> Authors { get; set; }
    public ICollection<Track> Tracks { get; set; }
    public ICollection<Like> TotalLikes { get; set; }
    public ICollection<Listen> TotalListens { get; set; }
    public ICollection<Share> TotalShares { get; set; }

    #endregion
}