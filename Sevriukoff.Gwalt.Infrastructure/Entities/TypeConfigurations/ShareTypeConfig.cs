using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Sevriukoff.Gwalt.Infrastructure.Entities.TypeConfigurations;

public class ShareTypeConfig : IEntityTypeConfiguration<Share>
{
    public void Configure(EntityTypeBuilder<Share> builder)
    {
        builder.HasOne(x => x.Track)
            .WithMany(x => x.TotalShares)
            .HasForeignKey(x => x.TrackId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(x => x.Album)
            .WithMany(x => x.Shares)
            .HasForeignKey(x => x.AlbumId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(x => x.Comment)
            .WithMany(x => x.TotalShares)
            .HasForeignKey(x => x.CommentId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Cascade);
    }
}