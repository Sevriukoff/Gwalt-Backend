using Sevriukoff.Gwalt.Infrastructure.Base;
using Sevriukoff.Gwalt.Infrastructure.Interfaces;

namespace Sevriukoff.Gwalt.Infrastructure.Entities;

public class User : BaseEntity, IHasListens, IHasLikes, IHasFollowers
{
    public string Name { get; set; }
    public string TsvectorName { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public string PasswordSalt { get; set; }
    public DateTime RegistrationDate { get; set; }
    public string AvatarUrl { get; set; }
    public string BackgroundUrl { get; set; }
    public string ShortDescription { get; set; }
    public string Description { get; set; }
    public int Age { get; set; }
    public Gender Gender { get; set; } = Gender.unknown;
    
    public int LikesCount { get; set; }
    public int SharesCount { get; set; }
    public int ListensCount { get; set; }
    public int FollowersCount { get; set; }
    public int FollowingCount { get; set; }

    #region NavigationProperties

    public ICollection<Album> Albums { get; set; }
    public ICollection<UserFollower> Followers { get; set; }
    public ICollection<UserFollower> Followings { get; set; }
    public ICollection<Like> Likes { get; set; }
    public ICollection<Listen> Listens { get; set; }

    #endregion
}

public enum Gender
{
    unknown,
    male,
    female
}