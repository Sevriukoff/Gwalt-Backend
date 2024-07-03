namespace Sevriukoff.Gwalt.WebApi.ViewModels;

public class UserWithStatViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public DateTime RegistrationDate { get; set; }
    public string AvatarUrl { get; set; }
    public string BackgroundUrl { get; set; }
    public string ShortDescription { get; set; }
    public string Description { get; set; }
    
    public int LikeCount { get; set; }
    public int ShareCount { get; set; }
    public int ListenCount { get; set; }
    public int FollowersCount { get; set; }
    public int FollowingCount { get; set; }
    public string[] TotalGenres { get; set; }
    public int TotalTracks { get; set; }
}