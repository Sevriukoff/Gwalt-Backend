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
    public Album Album { get; set; }
    
    public ICollection<Genre> Genres { get; set; } = new HashSet<Genre>();
}
