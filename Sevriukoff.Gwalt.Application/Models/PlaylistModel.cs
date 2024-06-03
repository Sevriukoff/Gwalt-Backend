using Sevriukoff.Gwalt.Application.Interfaces;

namespace Sevriukoff.Gwalt.Application.Models;

public class PlaylistModel : ILikeable, IShareable, IListenable
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsPrivate { get; set; }
    public string CoverUrl { get; set; }
    public UserModel User { get; set; }
    public TimeSpan Duration { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    
    public int LikesCount { get; set; }
    public int SharesCount { get; set; }
    public int ListensCount { get; set; }
    
    public List<TrackModel> Tracks { get; set; }
}