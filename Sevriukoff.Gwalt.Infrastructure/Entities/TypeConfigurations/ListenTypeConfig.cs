using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Sevriukoff.Gwalt.Infrastructure.Entities.TypeConfigurations;

public class ListenTypeConfig : IEntityTypeConfiguration<Listen>
{
    public void Configure(EntityTypeBuilder<Listen> builder)
    {
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
    }
}