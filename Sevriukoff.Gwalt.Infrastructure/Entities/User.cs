using Sevriukoff.Gwalt.Infrastructure.Base;

namespace Sevriukoff.Gwalt.Infrastructure.Entities;

public class User : BaseEntity
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public string PasswordSalt { get; set; }
    public DateTime RegistrationDate { get; set; }
    public string AvatarUrl { get; set; }
    public string BackgroundUrl { get; set; }
    public string ShortDescription { get; set; }
    public string Description { get; set; }

    #region NavigationProperties

    public ICollection<Album> Albums { get; set; }
    public ICollection<Like> TotalLikes { get; set; }
    public ICollection<Listen> TotalListens { get; set; }

    #endregion
}