namespace Sevriukoff.Gwalt.WebApi.ViewModels;

public class UserViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public DateTime RegistrationDate { get; set; }
    public string AvatarUrl { get; set; }
    public string BackgroundUrl { get; set; }
    public string ShortDescription { get; set; }
    public string Description { get; set; }

    public List<AlbumViewModel> Albums { get; set; }
}