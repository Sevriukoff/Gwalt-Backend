namespace Sevriukoff.Gwalt.WebApi.ViewModels;

public class AlbumViewModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string CoverUrl { get; set; }
    public bool IsSingle { get; set; }
    public bool IsExplicit { get; set; }
    public List<TrackViewModel> Tracks { get; set; }
}