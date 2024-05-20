namespace Sevriukoff.Gwalt.Infrastructure.Entities;

public class PlaylistTrack
{
    public int PlaylistId { get; set; }
    public Playlist Playlist { get; set; }
    
    public int TrackId { get; set; }
    public Track Track { get; set; }
    
    public int Order { get; set; }
}