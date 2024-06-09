using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sevriukoff.Gwalt.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EntityListenAddedNewFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<TimeSpan>(
                name: "ActiveListeningTime",
                table: "Listens",
                type: "interval",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "EndTime",
                table: "Listens",
                type: "interval",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<int>(
                name: "PauseCount",
                table: "Listens",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SeekCount",
                table: "Listens",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "TotalDuration",
                table: "Listens",
                type: "interval",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<int>(
                name: "Volume",
                table: "Listens",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActiveListeningTime",
                table: "Listens");

            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "Listens");

            migrationBuilder.DropColumn(
                name: "PauseCount",
                table: "Listens");

            migrationBuilder.DropColumn(
                name: "SeekCount",
                table: "Listens");

            migrationBuilder.DropColumn(
                name: "TotalDuration",
                table: "Listens");

            migrationBuilder.DropColumn(
                name: "Volume",
                table: "Listens");
        }
    }
}
