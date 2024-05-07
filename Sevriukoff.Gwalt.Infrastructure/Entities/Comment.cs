using Sevriukoff.Gwalt.Infrastructure.Base;

namespace Sevriukoff.Gwalt.Infrastructure.Entities;

public class Comment : BaseEntity
{
    public string Text { get; set; }
    public DateTime Date { get; set; }

    public int TrackId { get; set; }
    public int UserId { get; set; }
    
    #region NavigationProperties
    
    public Track Track { get; set; }
    public User User { get; set; }
    public ICollection<Like> TotalLikes { get; set; }
    public ICollection<Share> TotalShares { get; set; }

    #endregion
}