using Microsoft.EntityFrameworkCore;
using Sevriukoff.Gwalt.Infrastructure.Base;
using Sevriukoff.Gwalt.Infrastructure.Entities;
using Sevriukoff.Gwalt.Infrastructure.Entities.TypeConfigurations;

namespace Sevriukoff.Gwalt.Infrastructure;

public class DataDbContext : DbContext
{
    public DataDbContext(DbContextOptions<DataDbContext> options) : base(options) { }

    public const int UrlMaxLength = 650;
    
    public DbSet<User> Users { get; set; }
    public DbSet<Album> Albums { get; set; }
    public DbSet<Playlist> Playlists { get; set; }
    public DbSet<Track> Tracks { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Comment> Comments { get; set; }

    public DbSet<Like> Likes { get; set; }
    public DbSet<Listen> Listens { get; set; }
    public DbSet<Share> Shares { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new UserTypeConfig());
        modelBuilder.ApplyConfiguration(new AlbumTypeConfig());
        modelBuilder.ApplyConfiguration(new PlaylistTypeConfig());
        modelBuilder.ApplyConfiguration(new TrackTypeConfig());
        modelBuilder.ApplyConfiguration(new GenreTypeConfig());
        modelBuilder.ApplyConfiguration(new CommentTypeConfig());
        modelBuilder.ApplyConfiguration(new LikeTypeConfig());
        modelBuilder.ApplyConfiguration(new ListenTypeConfig());
        modelBuilder.ApplyConfiguration(new ShareTypeConfig());
    }
}