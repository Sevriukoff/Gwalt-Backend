using Sevriukoff.Gwalt.Application.Enums;
using Sevriukoff.Gwalt.Application.Interfaces;
using Sevriukoff.Gwalt.Infrastructure.Entities;

namespace Sevriukoff.Gwalt.Application.Models;

public class UserModel : BaseModel
{
    public string Name { get; set; }
    public string Email { get; set; }
    public DateTime RegistrationDate { get; set; }
    public string AvatarUrl { get; set; }
    public string BackgroundUrl { get; set; }
    public string ShortDescription { get; set; }
    public string Description { get; set; }
    public Gender Gender { get; set; }
    public int Age { get; set; }
    public bool IsArtist { get; set; }
    
    public int LikeCount { get; set; }
    public int ShareCount { get; set; }
    public int ListenCount { get; set; }
    public int FollowersCount { get; set; }
    public int FollowingCount { get; set; }
    
    /// <summary>
    /// Список пользователей, которые отслеживают данного пользователя.
    /// </summary>
    public List<UserModel> Followers { get; set; }
    
    /// <summary>
    /// Список пользователей, которых отслеживает данный пользователь.
    /// </summary>
    public List<UserModel> Followings { get; set; }
    
    /// <summary>
    /// Лайки, поставленные пользователем другим пользователем под их треки, альбомы, комметарии или под профилем.
    /// </summary>
    public List<LikeModel> Likes { get; set; }
    
    /// <summary>
    /// Прослушивания пользователя. Учитываются все listenable items, такие как треки и альбомы.
    /// </summary>
    public List<ListenModel> Listens { get; set; }
    
    /// <summary>
    /// Список объектов которыми пользователь поделился.
    /// Учитываются все shareable items, такие как треки, альбомы, комментарии.
    /// </summary>
    public List<ShareModel> Shares { get; set; }
    
    /// <summary>
    /// Список всех альбомов пользователя, в которых данный пользователь присутствует как автор.
    /// </summary>
    public List<AlbumModel> Albums { get; set; }

    /// <summary>
    /// Список всех коментариев которые оставил пользователь
    /// </summary>
    public List<CommentModel> Comments { get; set; }
    
    public UserModel(int id) : base(id) { }
    public UserModel() { }
}

public abstract class MetricModel
{
    public int Id { get; set; }
    public DateTime ReleaseDate { get; set; }
}

public class LikeModel : MetricModel
{
    public UserModel User { get; set; }
    public ILikeable Likeable { get; set; }
}

public class ListenModel : MetricModel
{
    public UserModel User { get; set; }
    public string SessionId { get; set; }
    public IListenable Listenable { get; set; }
    public ListenMetadata Metadata { get; set; }
    
    public ListenableType GetListenableType()
    {
        return Listenable is TrackModel ? ListenableType.Track : ListenableType.Album;
    }
}

public class ListenMetadata
{
    public int Quality { get; set; }
    
    public TimeSpan TotalDuration { get; set; }
    public TimeSpan EndTime  { get; set; }
    public TimeSpan ActiveListeningTime { get; set; }
    public int SeekCount { get; set; }
    public int PauseCount { get; set; }
    public int Volume { get; set; }
}

public class ShareModel : MetricModel
{
    public UserModel User { get; set; }
    public IShareable Shareable { get; set; }
}