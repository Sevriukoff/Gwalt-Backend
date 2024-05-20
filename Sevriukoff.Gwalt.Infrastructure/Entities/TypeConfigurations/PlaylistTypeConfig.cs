using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Sevriukoff.Gwalt.Infrastructure.Entities.TypeConfigurations;

public class PlaylistTypeConfig : IEntityTypeConfiguration<Playlist>
{
    public void Configure(EntityTypeBuilder<Playlist> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Title)
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(x => x.ImageUrl)
            .HasMaxLength(650)
            .IsRequired();
        
        builder.Property(x => x.IsPublic)
            .IsRequired();
        
        builder.HasOne(x => x.User)
            .WithMany()
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.Tracks)
            .WithMany()
            .UsingEntity<PlaylistTrack>(
                j => j
                    .HasOne(pt => pt.Track)
                    .WithMany()
                    .HasForeignKey(pt => pt.TrackId),
                j => j
                    .HasOne(pt => pt.Playlist)
                    .WithMany()
                    .HasForeignKey(pt => pt.PlaylistId),
                j =>
                {
                    j.HasKey(pt => new { pt.PlaylistId, pt.TrackId });
                    j.Property(pt => pt.Order).IsRequired();
                });
    }
}