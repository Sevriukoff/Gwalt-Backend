using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Sevriukoff.Gwalt.Infrastructure.Entities.TypeConfigurations;

public class TrackPeaksTypeConfig : IEntityTypeConfiguration<TrackPeaks>
{
    public void Configure(EntityTypeBuilder<TrackPeaks> builder)
    {
        builder.HasKey(x => x.TrackId);
        
        builder.HasOne(x => x.Track)
            .WithOne(x => x.Peaks)
            .HasForeignKey<TrackPeaks>(x => x.TrackId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}