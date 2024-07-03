namespace Sevriukoff.Gwalt.Infrastructure.Entities;

public class TrackPeaks
{
    public int TrackId { get; set; }
    public float[] Peaks { get; set; }
    
    public Track Track { get; set; }
}