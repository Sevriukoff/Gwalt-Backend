namespace Sevriukoff.Gwalt.WebApi.ViewModels;

public class TrackViewModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int Duration { get; set; }
    public bool IsExplicit { get; set; }
    public string AudioUrl { get; set; }
}