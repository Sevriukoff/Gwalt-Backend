using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Sevriukoff.Gwalt.Infrastructure.Entities.TypeConfigurations;

public class LikeTypeConfig : IEntityTypeConfiguration<Like>
{
    public void Configure(EntityTypeBuilder<Like> builder)
    {
        builder.HasOne(x => x.Track)
            .WithMany(x => x.Likes)
            .HasForeignKey(x => x.TrackId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(x => x.Album)
            .WithMany(x => x.Likes)
            .HasForeignKey(x => x.AlbumId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(x => x.Comment)
            .WithMany(x => x.TotalLikes)
            .HasForeignKey(x => x.CommentId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Cascade);
        
        // builder.HasOne(x => x.UserProfile)
        //     .WithMany(x => x.TotalLikes)
        //     .HasForeignKey(x => x.UserProfileId)
        //     .IsRequired(false)
        //     .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.User)
            .WithMany(x => x.Likes)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}