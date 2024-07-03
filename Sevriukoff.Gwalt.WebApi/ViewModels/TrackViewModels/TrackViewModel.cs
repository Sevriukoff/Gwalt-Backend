namespace Sevriukoff.Gwalt.WebApi.ViewModels;

public class TrackViewModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int Duration { get; set; }
    public bool IsExplicit { get; set; }
    public string AudioUrl { get; set; }
    public string CoverUrl { get; set; }
    public float[] Peaks { get; set; }
    public int AlbumId { get; set; }
    public UserViewModel[] Authors { get; set; }
}