using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sevriukoff.Gwalt.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EntityUserAddedSaltFromSpb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Metric_Users_UserProfileId",
                table: "Metric");

            migrationBuilder.AddColumn<string>(
                name: "PasswordSalt",
                table: "Users",
                type: "character varying(32)",
                maxLength: 32,
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Metric_Users_UserProfileId",
                table: "Metric",
                column: "UserProfileId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Metric_Users_UserProfileId",
                table: "Metric");

            migrationBuilder.DropColumn(
                name: "PasswordSalt",
                table: "Users");

            migrationBuilder.AddForeignKey(
                name: "FK_Metric_Users_UserProfileId",
                table: "Metric",
                column: "UserProfileId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
