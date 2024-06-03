namespace Sevriukoff.Gwalt.WebApi.ViewModels;

public class TrackCreateViewModel
{
    public string Title { get; set; }
    public bool IsExplicit { get; set; }
    public int[] Genres { get; set; }
    public string AudioUrl { get; set; }
    public int AlbumId { get; set; }
    public int Duration { get; set; }
}