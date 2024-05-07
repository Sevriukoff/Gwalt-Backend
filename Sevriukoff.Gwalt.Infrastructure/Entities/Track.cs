using Sevriukoff.Gwalt.Infrastructure.Base;

namespace Sevriukoff.Gwalt.Infrastructure.Entities;

public class Track : BaseEntity
{
    public string Title { get; set; }
    public int DurationInSeconds { get; set; }
    public bool IsExplicit { get; set; }
    public string AudioUrl { get; set; }
    public int Likes { get; set; }
    public int Shares { get; set; }
    public int Plays { get; set; }
    public int AlbumId { get; set; }

    #region NavigationProperties
    
    public Album Album { get; set; }
    public ICollection<Genre> Genres { get; set; } = new HashSet<Genre>();
    public ICollection<Like> TotalLikes { get; set; }
    public ICollection<Listen> TotalListens { get; set; }
    public ICollection<Share> TotalShares { get; set; }

    #endregion
}
