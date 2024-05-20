using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Sevriukoff.Gwalt.Infrastructure.Entities.TypeConfigurations;

public class AlbumTypeConfig : IEntityTypeConfiguration<Album>
{
    public void Configure(EntityTypeBuilder<Album> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.ImageUrl)
            .HasMaxLength(DataDbContext.UrlMaxLength)
            .IsRequired();
        
        builder.Property(x => x.Title)
            .HasMaxLength(255)
            .IsRequired();
        
        builder.Property(x => x.IsSingle)
            .IsRequired();
        
        builder.HasMany(x => x.Authors)
            .WithMany(x => x.Albums)
            .UsingEntity(j =>
            {
                j.ToTable("AlbumsUsers");
                j.Property<int>("AlbumId");
                j.Property<int>("UserId");
                j.HasKey("AlbumId", "UserId");
            });
        
        builder.HasMany(x => x.Tracks)
            .WithOne(x => x.Album)
            .HasForeignKey(x => x.AlbumId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}