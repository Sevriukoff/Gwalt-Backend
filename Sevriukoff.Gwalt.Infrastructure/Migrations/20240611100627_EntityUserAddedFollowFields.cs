using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sevriukoff.Gwalt.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EntityUserAddedFollowFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ShareCount",
                table: "Users",
                newName: "SharesCount");

            migrationBuilder.RenameColumn(
                name: "ListenCount",
                table: "Users",
                newName: "ListensCount");

            migrationBuilder.RenameColumn(
                name: "LikeCount",
                table: "Users",
                newName: "LikesCount");

            migrationBuilder.RenameColumn(
                name: "ShareCount",
                table: "Tracks",
                newName: "SharesCount");

            migrationBuilder.RenameColumn(
                name: "ListenCount",
                table: "Tracks",
                newName: "ListensCount");

            migrationBuilder.RenameColumn(
                name: "LikeCount",
                table: "Tracks",
                newName: "LikesCount");

            migrationBuilder.RenameColumn(
                name: "ShareCount",
                table: "Albums",
                newName: "SharesCount");

            migrationBuilder.RenameColumn(
                name: "ListenCount",
                table: "Albums",
                newName: "ListensCount");

            migrationBuilder.RenameColumn(
                name: "LikeCount",
                table: "Albums",
                newName: "LikesCount");

            migrationBuilder.AddColumn<int>(
                name: "FollowersCount",
                table: "Users",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FollowingCount",
                table: "Users",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FollowersCount",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "FollowingCount",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "SharesCount",
                table: "Users",
                newName: "ShareCount");

            migrationBuilder.RenameColumn(
                name: "ListensCount",
                table: "Users",
                newName: "ListenCount");

            migrationBuilder.RenameColumn(
                name: "LikesCount",
                table: "Users",
                newName: "LikeCount");

            migrationBuilder.RenameColumn(
                name: "SharesCount",
                table: "Tracks",
                newName: "ShareCount");

            migrationBuilder.RenameColumn(
                name: "ListensCount",
                table: "Tracks",
                newName: "ListenCount");

            migrationBuilder.RenameColumn(
                name: "LikesCount",
                table: "Tracks",
                newName: "LikeCount");

            migrationBuilder.RenameColumn(
                name: "SharesCount",
                table: "Albums",
                newName: "ShareCount");

            migrationBuilder.RenameColumn(
                name: "ListensCount",
                table: "Albums",
                newName: "ListenCount");

            migrationBuilder.RenameColumn(
                name: "LikesCount",
                table: "Albums",
                newName: "LikeCount");
        }
    }
}
