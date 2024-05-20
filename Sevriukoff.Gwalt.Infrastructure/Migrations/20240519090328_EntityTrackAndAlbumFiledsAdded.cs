using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sevriukoff.Gwalt.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EntityTrackAndAlbumFiledsAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Shares",
                table: "Tracks",
                newName: "ShareCount");

            migrationBuilder.RenameColumn(
                name: "Plays",
                table: "Tracks",
                newName: "PlayCount");

            migrationBuilder.RenameColumn(
                name: "Likes",
                table: "Tracks",
                newName: "LikeCount");

            migrationBuilder.AddColumn<int>(
                name: "LikeCount",
                table: "Albums",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PlayCount",
                table: "Albums",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ShareCount",
                table: "Albums",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LikeCount",
                table: "Albums");

            migrationBuilder.DropColumn(
                name: "PlayCount",
                table: "Albums");

            migrationBuilder.DropColumn(
                name: "ShareCount",
                table: "Albums");

            migrationBuilder.RenameColumn(
                name: "ShareCount",
                table: "Tracks",
                newName: "Shares");

            migrationBuilder.RenameColumn(
                name: "PlayCount",
                table: "Tracks",
                newName: "Plays");

            migrationBuilder.RenameColumn(
                name: "LikeCount",
                table: "Tracks",
                newName: "Likes");
        }
    }
}
