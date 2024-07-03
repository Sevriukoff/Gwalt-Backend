using Microsoft.EntityFrameworkCore.Migrations;
using Sevriukoff.Gwalt.Infrastructure.Entities;

#nullable disable

namespace Sevriukoff.Gwalt.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EntityUserAddedGender : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:public.gender", "unknown,male,female");

            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "Users",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Gender>(
                name: "Gender",
                table: "Users",
                type: "gender",
                nullable: false,
                defaultValue: Gender.unknown);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Age",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Users");

            migrationBuilder.AlterDatabase()
                .OldAnnotation("Npgsql:Enum:public.gender", "unknown,male,female");
        }
    }
}
