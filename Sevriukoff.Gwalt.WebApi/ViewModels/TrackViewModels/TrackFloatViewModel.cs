namespace Sevriukoff.Gwalt.WebApi.ViewModels;

public class TrackFloatViewModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int Duration { get; set; }
    public bool IsExplicit { get; set; }
    public string CoverUrl { get; set; }
    public string AudioUrl { get; set; }
    public DateTime ReleaseDate { get; set; }
    public float[] Peaks { get; set; }
    public string[] Genres { get; set; }
    public TrackAuthorsViewModel[] Authors { get; set; }
    
    public int LikesCount { get; set; }
    public int SharesCount { get; set; }
    public int ListensCount { get; set; }
}