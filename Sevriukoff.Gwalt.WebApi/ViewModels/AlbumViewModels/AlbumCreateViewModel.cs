namespace Sevriukoff.Gwalt.WebApi.ViewModels;

public class AlbumCreateViewModel
{
    public string Title { get; set; }
    public string CoverUrl { get; set; }
    public bool IsExplicit { get; set; }
    public bool IsSingle { get; set; }
    public string Description { get; set; }
    public DateTime ReleaseDate { get; set; }
}