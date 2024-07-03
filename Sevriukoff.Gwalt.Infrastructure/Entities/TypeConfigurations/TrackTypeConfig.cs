using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Sevriukoff.Gwalt.Infrastructure.Entities.TypeConfigurations;

public class TrackTypeConfig : IEntityTypeConfiguration<Track>
{
    public void Configure(EntityTypeBuilder<Track> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Title)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.TsvectorTitle)
            .HasComputedColumnSql("to_tsvector('russian', \"Title\")", true);
        
        builder.HasIndex(x => x.TsvectorTitle)
            .HasMethod("GIN")
            .HasOperators("gin_trgm_ops");
        
        builder.Property(x => x.AudioUrl)
            .HasMaxLength(DataDbContext.UrlMaxLength)
            .IsRequired();
        
        builder.Property(x => x.Duration)
            .IsRequired();
        
        builder.Property(x => x.IsExplicit)
            .IsRequired();
        
        builder.HasMany(x => x.Genres)
            .WithMany();
    }
}