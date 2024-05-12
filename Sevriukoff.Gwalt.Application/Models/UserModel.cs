using Sevriukoff.Gwalt.Application.Interfaces;

namespace Sevriukoff.Gwalt.Application.Models;

public class UserModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public DateTime RegistrationDate { get; set; }
    public string AvatarUrl { get; set; }
    public string BackgroundUrl { get; set; }
    public string ShortDescription { get; set; }
    public string Description { get; set; }
    public bool IsArtist { get; set; }
    
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
}

public abstract class MetricModel
{
    public int Id { get; set; }
    public DateTime ReleaseDate { get; set; }
}

public class LikeModel : MetricModel
{
    public ILikeable Likeable { get; set; }
}

public class ListenModel : MetricModel
{
    public IListenable Listenable { get; set; }
}

public class ShareModel : MetricModel
{
    public IShareable Shareable { get; set; }
}