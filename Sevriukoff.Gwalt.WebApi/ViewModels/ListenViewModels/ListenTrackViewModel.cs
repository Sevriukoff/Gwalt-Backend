namespace Sevriukoff.Gwalt.WebApi.ViewModels;

public class ListenTrackViewModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public bool IsExplicit { get; set; }
    public string CoverUrl { get; set; }
    public string[] Genres { get; set; }
    public TrackAuthorsViewModel[] Authors { get; set; }
    public int LikesCount { get; set; }
    public int SharesCount { get; set; }
    public int ListensCount { get; set; }
    

    public int ListenQuality { get; set; }
    public int ListenPercent { get; set; }
    public DateTime ListenTrackDate { get; set; }
}