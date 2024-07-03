using Sevriukoff.Gwalt.Infrastructure.Base;
using Sevriukoff.Gwalt.Infrastructure.Interfaces;

namespace Sevriukoff.Gwalt.Infrastructure.Entities;

public class Track : BaseEntity, IHasListens, IHasLikes
{
    public string Title { get; set; }
    public string TsvectorTitle { get; set; }
    public TimeSpan Duration { get; set; }
    public bool IsExplicit { get; set; }
    public string AudioUrl { get; set; }
    public int AlbumId { get; set; }
    
    public int LikesCount { get; set; }
    public int SharesCount { get; set; }
    public int ListensCount { get; set; }

    #region NavigationProperties

    public TrackPeaks Peaks { get; set; }
    public Album Album { get; set; }
    public ICollection<Genre> Genres { get; set; } = new HashSet<Genre>();
    public ICollection<Like> Likes { get; set; }
    public ICollection<Listen> Listens { get; set; }
    public ICollection<Share> Shares { get; set; }

    #endregion
}
