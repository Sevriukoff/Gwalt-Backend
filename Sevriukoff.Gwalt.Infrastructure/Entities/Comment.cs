namespace Sevriukoff.Gwalt.Infrastructure.Entities;

public class Comment
{
    public int Id { get; set; }

    public string Text { get; set; }
    public DateTime Date { get; set; }

    public int TrackId { get; set; }
    public Track Track { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
}