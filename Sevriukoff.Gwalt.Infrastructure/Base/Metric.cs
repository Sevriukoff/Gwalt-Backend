using Sevriukoff.Gwalt.Infrastructure.Entities;

namespace Sevriukoff.Gwalt.Infrastructure.Base;

public abstract class Metric : BaseEntity
{
    public int LikeById { get; set; }
    public User LikeBy { get; set; }
    
    public int? TrackId { get; set; }
    public Track Track { get; set; }
    
    public int? AlbumId { get; set; }
    public Album Album { get; set; }

    public DateTime ReleaseDate { get; set; }
}