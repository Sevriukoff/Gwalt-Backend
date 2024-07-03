namespace Sevriukoff.Gwalt.WebApi.ViewModels;

public class TrackSearchViewModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string CoverUrl { get; set; }
    public bool IsExplicit { get; set; }
    public int AlbumId { get; set; }
    public string[] Authors { get; set; }
}