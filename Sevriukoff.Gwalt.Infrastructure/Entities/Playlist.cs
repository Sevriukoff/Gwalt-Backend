using Sevriukoff.Gwalt.Infrastructure.Base;

namespace Sevriukoff.Gwalt.Infrastructure.Entities;

public class Playlist : BaseEntity
{
    public string Title { get; set; }
    public string TsvectorTitle { get; set; }
    public string ImageUrl { get; set; }
    public string Description { get; set; }
    public TimeSpan Duration { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int UserId { get; set; }
    public bool IsPublic { get; set; }

    public User User { get; set; }
    public ICollection<Track> Tracks { get; set; }
}