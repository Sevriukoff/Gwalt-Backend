using Sevriukoff.Gwalt.Infrastructure.Base;

namespace Sevriukoff.Gwalt.Infrastructure.Entities;

public class Album : BaseEntity
{
    public string Title { get; set; }
    public string ImageUrl { get; set; }
    public bool IsSingle { get; set; }
    public DateTime ReleaseDate { get; set; }
    
    public ICollection<User> Authors { get; set; }
    public ICollection<Track> Tracks { get; set; }
}