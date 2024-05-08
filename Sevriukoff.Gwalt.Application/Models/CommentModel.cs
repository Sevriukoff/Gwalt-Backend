using Sevriukoff.Gwalt.Application.Interfaces;
using Sevriukoff.Gwalt.Infrastructure.Entities;

namespace Sevriukoff.Gwalt.Application.Models;

public class CommentModel : ILikeable, IShareable
{
    public int Id { get; set; }
    public string Text { get; set; }
    public DateTime Date { get; set; }
    public TrackModel Track { get; set; }
    public User CommentBy { get; set; }
    
    public int LikesCount { get; set; }
    public int SharesCount { get; set; }
}