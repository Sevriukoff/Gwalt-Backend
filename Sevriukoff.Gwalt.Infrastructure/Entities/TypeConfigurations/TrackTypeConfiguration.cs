using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Sevriukoff.Gwalt.Infrastructure.Entities.TypeConfigurations;

public class TrackTypeConfiguration : IEntityTypeConfiguration<Track>
{
    public void Configure(EntityTypeBuilder<Track> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Title)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.AudioUrl)
            .HasMaxLength(255)
            .IsRequired();
        
        builder.Property(x => x.DurationInSeconds)
            .IsRequired();
        
        builder.Property(x => x.IsExplicit)
            .IsRequired();
        
        builder.HasMany(x => x.Genres)
            .WithMany();
    }
}