using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Sevriukoff.Gwalt.Infrastructure.Entities.TypeConfigurations;

public class UserTypeConfig : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();
        
        builder.Property(x => x.Name)
            .HasMaxLength(50)
            .IsRequired();
        
        builder.Property(x => x.TsvectorName)
            .HasComputedColumnSql("to_tsvector('russian', \"Name\")", true);

        builder.HasIndex(x => x.TsvectorName)
            .HasMethod("GIN")
            .HasOperators("gin_trgm_ops");
        
        builder.Property(x => x.Email)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.PasswordHash)
            .HasMaxLength(64)
            .IsRequired();
        
        builder.Property(x => x.PasswordSalt)
            .HasMaxLength(32)
            .IsRequired(false);

        builder.Property(x => x.AvatarUrl)
            .HasMaxLength(DataDbContext.UrlMaxLength)
            .IsRequired();
        
        builder.Property(x => x.BackgroundUrl)
            .HasMaxLength(DataDbContext.UrlMaxLength)
            .IsRequired();
        
        builder.Property(x => x.ShortDescription)
            .HasMaxLength(255)
            .IsRequired(false);
        
        builder.Property(x => x.Description)
            .HasMaxLength(2048)
            .IsRequired(false);
        
        builder.HasMany(x => x.Albums)
            .WithMany(x => x.Authors);
    }
}