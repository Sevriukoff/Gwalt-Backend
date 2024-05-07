using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Sevriukoff.Gwalt.Infrastructure.Entities.TypeConfigurations;

public class ListenTypeConfiguration : IEntityTypeConfiguration<Listen>
{
    public void Configure(EntityTypeBuilder<Listen> builder)
    {
        builder.HasOne(x => x.Track)
            .WithMany(x => x.TotalListens)
            .HasForeignKey(x => x.TrackId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(x => x.Album)
            .WithMany(x => x.TotalListens)
            .HasForeignKey(x => x.AlbumId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}