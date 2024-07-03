﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Sevriukoff.Gwalt.Infrastructure;
using Sevriukoff.Gwalt.Infrastructure.Entities;

#nullable disable

namespace Sevriukoff.Gwalt.Infrastructure.Migrations
{
    [DbContext(typeof(DataDbContext))]
    [Migration("20240621094038_EntityAlbumAddedNewFields")]
    partial class EntityAlbumAddedNewFields
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.18")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.HasPostgresEnum(modelBuilder, "public", "gender", new[] { "unknown", "male", "female" });
            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("AlbumUser", b =>
                {
                    b.Property<int>("AlbumId")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("AlbumId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("AlbumsUsers", (string)null);
                });

            modelBuilder.Entity("GenreTrack", b =>
                {
                    b.Property<int>("GenresId")
                        .HasColumnType("integer");

                    b.Property<int>("TrackId")
                        .HasColumnType("integer");

                    b.HasKey("GenresId", "TrackId");

                    b.HasIndex("TrackId");

                    b.ToTable("GenreTrack");
                });

            modelBuilder.Entity("Sevriukoff.Gwalt.Infrastructure.Entities.Album", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<TimeSpan>("Duration")
                        .HasColumnType("interval");

                    b.Property<int>("GenreId")
                        .HasColumnType("integer");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasMaxLength(650)
                        .HasColumnType("character varying(650)");

                    b.Property<bool>("IsSingle")
                        .HasColumnType("boolean");

                    b.Property<int>("LikesCount")
                        .HasColumnType("integer");

                    b.Property<int>("ListensCount")
                        .HasColumnType("integer");

                    b.Property<DateTime>("ReleaseDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("SharesCount")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<int>("TracksCount")
                        .HasColumnType("integer");

                    b.Property<string>("TsvectorTitle")
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("text")
                        .HasComputedColumnSql("to_tsvector('russian', \"Title\")", true);

                    b.HasKey("Id");

                    b.HasIndex("GenreId");

                    b.HasIndex("TsvectorTitle");

                    NpgsqlIndexBuilderExtensions.HasMethod(b.HasIndex("TsvectorTitle"), "GIN");
                    NpgsqlIndexBuilderExtensions.HasOperators(b.HasIndex("TsvectorTitle"), new[] { "gin_trgm_ops" });

                    b.ToTable("Albums");
                });

            modelBuilder.Entity("Sevriukoff.Gwalt.Infrastructure.Entities.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasMaxLength(2048)
                        .HasColumnType("character varying(2048)");

                    b.Property<int>("TrackId")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("TrackId");

                    b.HasIndex("UserId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("Sevriukoff.Gwalt.Infrastructure.Entities.Genre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.ToTable("Genres");
                });

            modelBuilder.Entity("Sevriukoff.Gwalt.Infrastructure.Entities.Like", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("AlbumId")
                        .HasColumnType("integer");

                    b.Property<int?>("CommentId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("ReleaseDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("TrackId")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<int?>("UserProfileId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("AlbumId");

                    b.HasIndex("CommentId");

                    b.HasIndex("TrackId");

                    b.HasIndex("UserId");

                    b.HasIndex("UserProfileId");

                    b.ToTable("Likes");
                });

            modelBuilder.Entity("Sevriukoff.Gwalt.Infrastructure.Entities.Listen", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<TimeSpan>("ActiveListeningTime")
                        .HasColumnType("interval");

                    b.Property<int?>("AlbumId")
                        .HasColumnType("integer");

                    b.Property<TimeSpan>("EndTime")
                        .HasColumnType("interval");

                    b.Property<int>("PauseCount")
                        .HasColumnType("integer");

                    b.Property<int>("Quality")
                        .HasColumnType("integer");

                    b.Property<DateTime>("ReleaseDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("SeekCount")
                        .HasColumnType("integer");

                    b.Property<string>("SessionId")
                        .HasMaxLength(36)
                        .HasColumnType("character varying(36)");

                    b.Property<TimeSpan>("TotalDuration")
                        .HasColumnType("interval");

                    b.Property<int?>("TrackId")
                        .HasColumnType("integer");

                    b.Property<int?>("UserId")
                        .HasColumnType("integer");

                    b.Property<int>("Volume")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("AlbumId");

                    b.HasIndex("TrackId");

                    b.HasIndex("UserId");

                    b.ToTable("Listens");
                });

            modelBuilder.Entity("Sevriukoff.Gwalt.Infrastructure.Entities.Playlist", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<TimeSpan>("Duration")
                        .HasColumnType("interval");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasMaxLength(650)
                        .HasColumnType("character varying(650)");

                    b.Property<bool>("IsPublic")
                        .HasColumnType("boolean");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("TsvectorTitle")
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("text")
                        .HasComputedColumnSql("to_tsvector('russian', \"Title\")", true);

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("TsvectorTitle");

                    NpgsqlIndexBuilderExtensions.HasMethod(b.HasIndex("TsvectorTitle"), "GIN");
                    NpgsqlIndexBuilderExtensions.HasOperators(b.HasIndex("TsvectorTitle"), new[] { "gin_trgm_ops" });

                    b.HasIndex("UserId");

                    b.ToTable("Playlists");
                });

            modelBuilder.Entity("Sevriukoff.Gwalt.Infrastructure.Entities.PlaylistTrack", b =>
                {
                    b.Property<int>("PlaylistId")
                        .HasColumnType("integer");

                    b.Property<int>("TrackId")
                        .HasColumnType("integer");

                    b.Property<int>("Order")
                        .HasColumnType("integer");

                    b.HasKey("PlaylistId", "TrackId");

                    b.HasIndex("TrackId");

                    b.ToTable("PlaylistTrack");
                });

            modelBuilder.Entity("Sevriukoff.Gwalt.Infrastructure.Entities.Share", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("AlbumId")
                        .HasColumnType("integer");

                    b.Property<int?>("CommentId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("ReleaseDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("ShareToUrl")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("TrackId")
                        .HasColumnType("integer");

                    b.Property<int?>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("AlbumId");

                    b.HasIndex("CommentId");

                    b.HasIndex("TrackId");

                    b.HasIndex("UserId");

                    b.ToTable("Shares");
                });

            modelBuilder.Entity("Sevriukoff.Gwalt.Infrastructure.Entities.Track", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AlbumId")
                        .HasColumnType("integer");

                    b.Property<string>("AudioUrl")
                        .IsRequired()
                        .HasMaxLength(650)
                        .HasColumnType("character varying(650)");

                    b.Property<TimeSpan>("Duration")
                        .HasColumnType("interval");

                    b.Property<bool>("IsExplicit")
                        .HasColumnType("boolean");

                    b.Property<int>("LikesCount")
                        .HasColumnType("integer");

                    b.Property<int>("ListensCount")
                        .HasColumnType("integer");

                    b.Property<int>("SharesCount")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("TsvectorTitle")
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("text")
                        .HasComputedColumnSql("to_tsvector('russian', \"Title\")", true);

                    b.HasKey("Id");

                    b.HasIndex("AlbumId");

                    b.HasIndex("TsvectorTitle");

                    NpgsqlIndexBuilderExtensions.HasMethod(b.HasIndex("TsvectorTitle"), "GIN");
                    NpgsqlIndexBuilderExtensions.HasOperators(b.HasIndex("TsvectorTitle"), new[] { "gin_trgm_ops" });

                    b.ToTable("Tracks");
                });

            modelBuilder.Entity("Sevriukoff.Gwalt.Infrastructure.Entities.TrackPeaks", b =>
                {
                    b.Property<int>("TrackId")
                        .HasColumnType("integer");

                    b.Property<float[]>("Peaks")
                        .IsRequired()
                        .HasColumnType("real[]");

                    b.HasKey("TrackId");

                    b.ToTable("TrackPeaks");
                });

            modelBuilder.Entity("Sevriukoff.Gwalt.Infrastructure.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Age")
                        .HasColumnType("integer");

                    b.Property<string>("AvatarUrl")
                        .IsRequired()
                        .HasMaxLength(650)
                        .HasColumnType("character varying(650)");

                    b.Property<string>("BackgroundUrl")
                        .IsRequired()
                        .HasMaxLength(650)
                        .HasColumnType("character varying(650)");

                    b.Property<string>("Description")
                        .HasMaxLength(2048)
                        .HasColumnType("character varying(2048)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<int>("FollowersCount")
                        .HasColumnType("integer");

                    b.Property<int>("FollowingCount")
                        .HasColumnType("integer");

                    b.Property<Gender>("Gender")
                        .HasColumnType("gender");

                    b.Property<int>("LikesCount")
                        .HasColumnType("integer");

                    b.Property<int>("ListensCount")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)");

                    b.Property<string>("PasswordSalt")
                        .HasMaxLength(32)
                        .HasColumnType("character varying(32)");

                    b.Property<DateTime>("RegistrationDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("SharesCount")
                        .HasColumnType("integer");

                    b.Property<string>("ShortDescription")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("TsvectorName")
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("text")
                        .HasComputedColumnSql("to_tsvector('russian', \"Name\")", true);

                    b.HasKey("Id");

                    b.HasIndex("TsvectorName");

                    NpgsqlIndexBuilderExtensions.HasMethod(b.HasIndex("TsvectorName"), "GIN");
                    NpgsqlIndexBuilderExtensions.HasOperators(b.HasIndex("TsvectorName"), new[] { "gin_trgm_ops" });

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Sevriukoff.Gwalt.Infrastructure.Entities.UserFollower", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<int>("FollowerId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("UserId", "FollowerId");

                    b.HasIndex("FollowerId");

                    b.ToTable("UserFollowers");
                });

            modelBuilder.Entity("AlbumUser", b =>
                {
                    b.HasOne("Sevriukoff.Gwalt.Infrastructure.Entities.Album", null)
                        .WithMany()
                        .HasForeignKey("AlbumId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Sevriukoff.Gwalt.Infrastructure.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GenreTrack", b =>
                {
                    b.HasOne("Sevriukoff.Gwalt.Infrastructure.Entities.Genre", null)
                        .WithMany()
                        .HasForeignKey("GenresId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Sevriukoff.Gwalt.Infrastructure.Entities.Track", null)
                        .WithMany()
                        .HasForeignKey("TrackId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Sevriukoff.Gwalt.Infrastructure.Entities.Album", b =>
                {
                    b.HasOne("Sevriukoff.Gwalt.Infrastructure.Entities.Genre", "Genre")
                        .WithMany()
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Genre");
                });

            modelBuilder.Entity("Sevriukoff.Gwalt.Infrastructure.Entities.Comment", b =>
                {
                    b.HasOne("Sevriukoff.Gwalt.Infrastructure.Entities.Track", "Track")
                        .WithMany()
                        .HasForeignKey("TrackId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Sevriukoff.Gwalt.Infrastructure.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Track");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Sevriukoff.Gwalt.Infrastructure.Entities.Like", b =>
                {
                    b.HasOne("Sevriukoff.Gwalt.Infrastructure.Entities.Album", "Album")
                        .WithMany("Likes")
                        .HasForeignKey("AlbumId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Sevriukoff.Gwalt.Infrastructure.Entities.Comment", "Comment")
                        .WithMany("TotalLikes")
                        .HasForeignKey("CommentId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Sevriukoff.Gwalt.Infrastructure.Entities.Track", "Track")
                        .WithMany("Likes")
                        .HasForeignKey("TrackId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Sevriukoff.Gwalt.Infrastructure.Entities.User", "User")
                        .WithMany("Likes")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Sevriukoff.Gwalt.Infrastructure.Entities.User", "UserProfile")
                        .WithMany()
                        .HasForeignKey("UserProfileId");

                    b.Navigation("Album");

                    b.Navigation("Comment");

                    b.Navigation("Track");

                    b.Navigation("User");

                    b.Navigation("UserProfile");
                });

            modelBuilder.Entity("Sevriukoff.Gwalt.Infrastructure.Entities.Listen", b =>
                {
                    b.HasOne("Sevriukoff.Gwalt.Infrastructure.Entities.Album", "Album")
                        .WithMany("Listens")
                        .HasForeignKey("AlbumId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Sevriukoff.Gwalt.Infrastructure.Entities.Track", "Track")
                        .WithMany("Listens")
                        .HasForeignKey("TrackId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Sevriukoff.Gwalt.Infrastructure.Entities.User", "User")
                        .WithMany("Listens")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Album");

                    b.Navigation("Track");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Sevriukoff.Gwalt.Infrastructure.Entities.Playlist", b =>
                {
                    b.HasOne("Sevriukoff.Gwalt.Infrastructure.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Sevriukoff.Gwalt.Infrastructure.Entities.PlaylistTrack", b =>
                {
                    b.HasOne("Sevriukoff.Gwalt.Infrastructure.Entities.Playlist", "Playlist")
                        .WithMany()
                        .HasForeignKey("PlaylistId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Sevriukoff.Gwalt.Infrastructure.Entities.Track", "Track")
                        .WithMany()
                        .HasForeignKey("TrackId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Playlist");

                    b.Navigation("Track");
                });

            modelBuilder.Entity("Sevriukoff.Gwalt.Infrastructure.Entities.Share", b =>
                {
                    b.HasOne("Sevriukoff.Gwalt.Infrastructure.Entities.Album", "Album")
                        .WithMany("Shares")
                        .HasForeignKey("AlbumId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Sevriukoff.Gwalt.Infrastructure.Entities.Comment", "Comment")
                        .WithMany("TotalShares")
                        .HasForeignKey("CommentId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Sevriukoff.Gwalt.Infrastructure.Entities.Track", "Track")
                        .WithMany("Shares")
                        .HasForeignKey("TrackId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Sevriukoff.Gwalt.Infrastructure.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("Album");

                    b.Navigation("Comment");

                    b.Navigation("Track");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Sevriukoff.Gwalt.Infrastructure.Entities.Track", b =>
                {
                    b.HasOne("Sevriukoff.Gwalt.Infrastructure.Entities.Album", "Album")
                        .WithMany("Tracks")
                        .HasForeignKey("AlbumId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Album");
                });

            modelBuilder.Entity("Sevriukoff.Gwalt.Infrastructure.Entities.TrackPeaks", b =>
                {
                    b.HasOne("Sevriukoff.Gwalt.Infrastructure.Entities.Track", "Track")
                        .WithOne("Peaks")
                        .HasForeignKey("Sevriukoff.Gwalt.Infrastructure.Entities.TrackPeaks", "TrackId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Track");
                });

            modelBuilder.Entity("Sevriukoff.Gwalt.Infrastructure.Entities.UserFollower", b =>
                {
                    b.HasOne("Sevriukoff.Gwalt.Infrastructure.Entities.User", "Follower")
                        .WithMany("Followings")
                        .HasForeignKey("FollowerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Sevriukoff.Gwalt.Infrastructure.Entities.User", "User")
                        .WithMany("Followers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Follower");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Sevriukoff.Gwalt.Infrastructure.Entities.Album", b =>
                {
                    b.Navigation("Likes");

                    b.Navigation("Listens");

                    b.Navigation("Shares");

                    b.Navigation("Tracks");
                });

            modelBuilder.Entity("Sevriukoff.Gwalt.Infrastructure.Entities.Comment", b =>
                {
                    b.Navigation("TotalLikes");

                    b.Navigation("TotalShares");
                });

            modelBuilder.Entity("Sevriukoff.Gwalt.Infrastructure.Entities.Track", b =>
                {
                    b.Navigation("Likes");

                    b.Navigation("Listens");

                    b.Navigation("Peaks")
                        .IsRequired();

                    b.Navigation("Shares");
                });

            modelBuilder.Entity("Sevriukoff.Gwalt.Infrastructure.Entities.User", b =>
                {
                    b.Navigation("Followers");

                    b.Navigation("Followings");

                    b.Navigation("Likes");

                    b.Navigation("Listens");
                });
#pragma warning restore 612, 618
        }
    }
}
