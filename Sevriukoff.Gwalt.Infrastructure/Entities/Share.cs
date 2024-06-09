using Sevriukoff.Gwalt.Infrastructure.Base;

namespace Sevriukoff.Gwalt.Infrastructure.Entities;

public class Share : Metric
{
    public int? UserId { get; set; } 
    public User User { get; set; }
    
    public int? TrackId { get; set; }
    public Track Track { get; set; }
    
    public int? AlbumId { get; set; }
    public Album Album { get; set; }
    
    public int? CommentId { get; set; }
    public Comment Comment { get; set; }
    
    
    public string ShareToUrl { get; set; }
}