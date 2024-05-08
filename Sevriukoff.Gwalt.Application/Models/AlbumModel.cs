using Sevriukoff.Gwalt.Application.Interfaces;

namespace Sevriukoff.Gwalt.Application.Models;

public class AlbumModel : ILikeable, IListenable, IShareable
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string ImageUrl { get; set; }
    public bool IsSingle { get; set; }
    public bool IsExplicit { get; set; }
    public DateTime ReleaseDate { get; set; }
    
    public int LikesCount { get; set; }
    public int ListensCount { get; set; }
    public int SharesCount { get; set; }

    public List<TrackModel> Tracks { get; set; }
}