using Sevriukoff.Gwalt.Infrastructure.Base;

namespace Sevriukoff.Gwalt.Infrastructure.Entities;

public class Listen : Metric
{
    public int? UserId { get; set; } 
    public virtual User User { get; set; }

    public string? SessionId { get; set; }
    
    public int? TrackId { get; set; }
    public Track Track { get; set; }
    
    public int? AlbumId { get; set; }
    public Album Album { get; set; }
    
    public TimeSpan TotalDuration { get; set; }
    public TimeSpan EndTime  { get; set; }
    public TimeSpan ActiveListeningTime { get; set; }
    public int SeekCount { get; set; }
    public int PauseCount { get; set; }
    public int Volume { get; set; }
    public int Quality { get; set; }
}