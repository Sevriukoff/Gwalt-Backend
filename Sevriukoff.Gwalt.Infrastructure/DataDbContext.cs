using Microsoft.EntityFrameworkCore;
using Sevriukoff.Gwalt.Infrastructure.Base;
using Sevriukoff.Gwalt.Infrastructure.Entities;
using Sevriukoff.Gwalt.Infrastructure.Entities.TypeConfigurations;

namespace Sevriukoff.Gwalt.Infrastructure;

public class DataDbContext : DbContext
{
    public DataDbContext(DbContextOptions<DataDbContext> options) : base(options) { }
    
    public DbSet<User> Users { get; set; }
    public DbSet<Album> Albums { get; set; }
    public DbSet<Track> Tracks { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Comment> Comments { get; set; }

    public DbSet<Like> Likes { get; set; }
    public DbSet<Listen> Listens { get; set; }
    public DbSet<Share> Shares { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new UserTypeConfiguration());
        modelBuilder.ApplyConfiguration(new AlbumTypeConfiguration());
        modelBuilder.ApplyConfiguration(new TrackTypeConfiguration());
        modelBuilder.ApplyConfiguration(new GenreTypeConfiguration());
        modelBuilder.ApplyConfiguration(new CommentTypeConfiguration());
        modelBuilder.ApplyConfiguration(new LikeTypeConfiguration());
        modelBuilder.ApplyConfiguration(new ListenTypeConfiguration());
        modelBuilder.ApplyConfiguration(new ShareTypeConfiguration());

        modelBuilder.Entity<Metric>().UseTphMappingStrategy();
    }
}