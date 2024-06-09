using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sevriukoff.Gwalt.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EntityListenUpdatedForeignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Listens_Users_UserId",
                table: "Listens");

            migrationBuilder.AlterColumn<string>(
                name: "SessionId",
                table: "Listens",
                type: "character varying(36)",
                maxLength: 36,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Listens_Users_UserId",
                table: "Listens",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Listens_Users_UserId",
                table: "Listens");

            migrationBuilder.AlterColumn<string>(
                name: "SessionId",
                table: "Listens",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(36)",
                oldMaxLength: 36,
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Listens_Users_UserId",
                table: "Listens",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
