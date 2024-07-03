using Sevriukoff.Gwalt.Application.Interfaces;

namespace Sevriukoff.Gwalt.Application.Models;

public class TrackModel : ILikeable, IListenable, IShareable
{
    public int Id { get; set; }
    public string Title { get; set; }
    public TimeSpan Duration { get; set; }
    public bool IsExplicit { get; set; }
    public string AudioUrl { get; set; }
    public GenreModel[] Genres { get; set; }
    public float[] Peaks { get; set; }
    public AlbumModel Album { get; set; }
    
    public int LikesCount { get; set; }
    public int ListensCount { get; set; }
    public int SharesCount { get; set; }
}