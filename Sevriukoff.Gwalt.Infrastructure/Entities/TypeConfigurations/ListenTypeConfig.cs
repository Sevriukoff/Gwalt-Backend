using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Sevriukoff.Gwalt.Infrastructure.Entities.TypeConfigurations;

public class ListenTypeConfig : IEntityTypeConfiguration<Listen>
{
    public void Configure(EntityTypeBuilder<Listen> builder)
    {
        builder.HasKey(l => l.Id);

        builder.Navigation(x => x.User)
            .IsRequired(false);

        builder.Property(x => x.SessionId)
            .IsRequired(false)
            .HasMaxLength(36);
        
        builder.Property(x => x.Quality)
            .IsRequired(true);

        builder.Property(x => x.ActiveListeningTime)
            .IsRequired(true);
        
        builder.Property(x => x.EndTime)
            .IsRequired(true);
        
        builder.Property(x => x.PauseCount)
            .IsRequired(true);
        
        builder.Property(x => x.SeekCount)
            .IsRequired(true);
        
        builder.Property(x => x.TotalDuration)
            .IsRequired(true);

        builder.Property(x => x.Volume)
            .IsRequired(true);
        
        builder.HasOne(x => x.Track)
            .WithMany(x => x.TotalListens)
            .HasForeignKey(x => x.TrackId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(x => x.Album)
            .WithMany(x => x.Listens)
            .HasForeignKey(x => x.AlbumId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(x => x.User)
            .WithMany(x => x.TotalListens)
            .HasForeignKey(x => x.UserId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Cascade);
    }
}