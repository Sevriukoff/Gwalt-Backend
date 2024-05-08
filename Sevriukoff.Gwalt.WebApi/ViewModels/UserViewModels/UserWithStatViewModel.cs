namespace Sevriukoff.Gwalt.WebApi.ViewModels.UserViewModels;

public class UserWithStatViewModel : UserViewModel
{
    public string[] Genres { get; set; }
    public int TotalTracks { get; set; }
    public int TotalLikes { get; set; }
    public int TotalPlays { get; set; }
}