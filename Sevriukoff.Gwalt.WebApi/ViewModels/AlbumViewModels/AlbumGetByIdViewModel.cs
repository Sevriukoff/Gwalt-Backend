namespace Sevriukoff.Gwalt.WebApi.ViewModels;

public class AlbumGetByIdViewModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string CoverUrl { get; set; }
    public bool IsSingle { get; set; }
    public bool IsExplicit { get; set; }
    public DateTime ReleaseDate { get; set; }
    public string ReleaseString { get; set; }
    public string Genre { get; set; }
    
    public int Duration { get; set; }
    public int TracksCount { get; set; }
    public int LikesCount { get; set; }
    public int ListensCount { get; set; }
    public int SharesCount { get; set; }
}