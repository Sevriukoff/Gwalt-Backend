namespace Sevriukoff.Gwalt.Infrastructure.Entities;

public class UserFollower
{
    public int UserId { get; set; }
    public User? User { get; set; }

    public int FollowerId { get; set; }
    public User? Follower { get; set; }

    public DateTime CreatedAt { get; set; }
}