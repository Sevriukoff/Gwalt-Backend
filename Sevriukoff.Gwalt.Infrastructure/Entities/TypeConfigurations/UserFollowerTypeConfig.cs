using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Sevriukoff.Gwalt.Infrastructure.Entities.TypeConfigurations;

public class UserFollowerTypeConfig : IEntityTypeConfiguration<UserFollower>
{
    public void Configure(EntityTypeBuilder<UserFollower> builder)
    {
        builder.HasKey(x => new {x.UserId, x.FollowerId});
        
        builder.HasOne(x => x.User)
            .WithMany(x => x.Followers)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasOne(x => x.Follower)
            .WithMany(x => x.Followings)
            .HasForeignKey(x => x.FollowerId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}