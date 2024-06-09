using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Sevriukoff.Gwalt.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EntityMetricUpdateInheritance : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Metric_Albums_AlbumId",
                table: "Metric");

            migrationBuilder.DropForeignKey(
                name: "FK_Metric_Comments_CommentId",
                table: "Metric");

            migrationBuilder.DropForeignKey(
                name: "FK_Metric_Tracks_TrackId",
                table: "Metric");

            migrationBuilder.DropForeignKey(
                name: "FK_Metric_Users_LikeById",
                table: "Metric");

            migrationBuilder.DropForeignKey(
                name: "FK_Metric_Users_UserProfileId",
                table: "Metric");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Metric",
                table: "Metric");

            migrationBuilder.DropIndex(
                name: "IX_Metric_LikeById",
                table: "Metric");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Metric");

            migrationBuilder.DropColumn(
                name: "LikeById",
                table: "Metric");

            migrationBuilder.DropColumn(
                name: "Quality",
                table: "Metric");

            migrationBuilder.RenameTable(
                name: "Metric",
                newName: "Shares");

            migrationBuilder.RenameColumn(
                name: "UserProfileId",
                table: "Shares",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Metric_UserProfileId",
                table: "Shares",
                newName: "IX_Shares_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Metric_TrackId",
                table: "Shares",
                newName: "IX_Shares_TrackId");

            migrationBuilder.RenameIndex(
                name: "IX_Metric_CommentId",
                table: "Shares",
                newName: "IX_Shares_CommentId");

            migrationBuilder.RenameIndex(
                name: "IX_Metric_AlbumId",
                table: "Shares",
                newName: "IX_Shares_AlbumId");

            migrationBuilder.AlterColumn<string>(
                name: "ShareToUrl",
                table: "Shares",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Shares",
                table: "Shares",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Likes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    TrackId = table.Column<int>(type: "integer", nullable: true),
                    AlbumId = table.Column<int>(type: "integer", nullable: true),
                    CommentId = table.Column<int>(type: "integer", nullable: true),
                    UserProfileId = table.Column<int>(type: "integer", nullable: true),
                    ReleaseDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Likes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Likes_Albums_AlbumId",
                        column: x => x.AlbumId,
                        principalTable: "Albums",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Likes_Comments_CommentId",
                        column: x => x.CommentId,
                        principalTable: "Comments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Likes_Tracks_TrackId",
                        column: x => x.TrackId,
                        principalTable: "Tracks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Likes_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Likes_Users_UserProfileId",
                        column: x => x.UserProfileId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Listens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: true),
                    SessionId = table.Column<string>(type: "text", nullable: true),
                    TrackId = table.Column<int>(type: "integer", nullable: true),
                    AlbumId = table.Column<int>(type: "integer", nullable: true),
                    Quality = table.Column<int>(type: "integer", nullable: false),
                    ReleaseDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Listens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Listens_Albums_AlbumId",
                        column: x => x.AlbumId,
                        principalTable: "Albums",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Listens_Tracks_TrackId",
                        column: x => x.TrackId,
                        principalTable: "Tracks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Listens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Likes_AlbumId",
                table: "Likes",
                column: "AlbumId");

            migrationBuilder.CreateIndex(
                name: "IX_Likes_CommentId",
                table: "Likes",
                column: "CommentId");

            migrationBuilder.CreateIndex(
                name: "IX_Likes_TrackId",
                table: "Likes",
                column: "TrackId");

            migrationBuilder.CreateIndex(
                name: "IX_Likes_UserId",
                table: "Likes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Likes_UserProfileId",
                table: "Likes",
                column: "UserProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_Listens_AlbumId",
                table: "Listens",
                column: "AlbumId");

            migrationBuilder.CreateIndex(
                name: "IX_Listens_TrackId",
                table: "Listens",
                column: "TrackId");

            migrationBuilder.CreateIndex(
                name: "IX_Listens_UserId",
                table: "Listens",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Shares_Albums_AlbumId",
                table: "Shares",
                column: "AlbumId",
                principalTable: "Albums",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Shares_Comments_CommentId",
                table: "Shares",
                column: "CommentId",
                principalTable: "Comments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Shares_Tracks_TrackId",
                table: "Shares",
                column: "TrackId",
                principalTable: "Tracks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Shares_Users_UserId",
                table: "Shares",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shares_Albums_AlbumId",
                table: "Shares");

            migrationBuilder.DropForeignKey(
                name: "FK_Shares_Comments_CommentId",
                table: "Shares");

            migrationBuilder.DropForeignKey(
                name: "FK_Shares_Tracks_TrackId",
                table: "Shares");

            migrationBuilder.DropForeignKey(
                name: "FK_Shares_Users_UserId",
                table: "Shares");

            migrationBuilder.DropTable(
                name: "Likes");

            migrationBuilder.DropTable(
                name: "Listens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Shares",
                table: "Shares");

            migrationBuilder.RenameTable(
                name: "Shares",
                newName: "Metric");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Metric",
                newName: "UserProfileId");

            migrationBuilder.RenameIndex(
                name: "IX_Shares_UserId",
                table: "Metric",
                newName: "IX_Metric_UserProfileId");

            migrationBuilder.RenameIndex(
                name: "IX_Shares_TrackId",
                table: "Metric",
                newName: "IX_Metric_TrackId");

            migrationBuilder.RenameIndex(
                name: "IX_Shares_CommentId",
                table: "Metric",
                newName: "IX_Metric_CommentId");

            migrationBuilder.RenameIndex(
                name: "IX_Shares_AlbumId",
                table: "Metric",
                newName: "IX_Metric_AlbumId");

            migrationBuilder.AlterColumn<string>(
                name: "ShareToUrl",
                table: "Metric",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Metric",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "LikeById",
                table: "Metric",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Quality",
                table: "Metric",
                type: "integer",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Metric",
                table: "Metric",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Metric_LikeById",
                table: "Metric",
                column: "LikeById");

            migrationBuilder.AddForeignKey(
                name: "FK_Metric_Albums_AlbumId",
                table: "Metric",
                column: "AlbumId",
                principalTable: "Albums",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Metric_Comments_CommentId",
                table: "Metric",
                column: "CommentId",
                principalTable: "Comments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Metric_Tracks_TrackId",
                table: "Metric",
                column: "TrackId",
                principalTable: "Tracks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Metric_Users_LikeById",
                table: "Metric",
                column: "LikeById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Metric_Users_UserProfileId",
                table: "Metric",
                column: "UserProfileId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
