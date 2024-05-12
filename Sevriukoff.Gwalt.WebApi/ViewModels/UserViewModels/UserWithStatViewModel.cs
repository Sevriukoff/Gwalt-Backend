namespace Sevriukoff.Gwalt.WebApi.ViewModels;

public class UserWithStatViewModel : UserViewModel
{
    public string[] TotalGenres { get; set; }
    public int TotalTracks { get; set; }
    public int TotalLikes { get; set; }
    public int TotalListens { get; set; }
}