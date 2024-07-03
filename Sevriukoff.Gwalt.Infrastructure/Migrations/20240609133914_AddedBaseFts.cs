using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sevriukoff.Gwalt.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedBaseFts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TsvectorName",
                table: "Users",
                type: "text",
                nullable: false,
                computedColumnSql: "to_tsvector('russian', \"Name\")",
                stored: true);

            migrationBuilder.AddColumn<string>(
                name: "TsvectorTitle",
                table: "Tracks",
                type: "text",
                nullable: false,
                computedColumnSql: "to_tsvector('russian', \"Title\")",
                stored: true);

            migrationBuilder.AddColumn<string>(
                name: "TsvectorTitle",
                table: "Playlists",
                type: "text",
                nullable: false,
                computedColumnSql: "to_tsvector('russian', \"Title\")",
                stored: true);

            migrationBuilder.AddColumn<string>(
                name: "TsvectorTitle",
                table: "Albums",
                type: "text",
                nullable: false,
                computedColumnSql: "to_tsvector('russian', \"Title\")",
                stored: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_TsvectorName",
                table: "Users",
                column: "TsvectorName")
                .Annotation("Npgsql:IndexMethod", "GIN")
                .Annotation("Npgsql:IndexOperators", new[] { "gin_trgm_ops" });

            migrationBuilder.CreateIndex(
                name: "IX_Tracks_TsvectorTitle",
                table: "Tracks",
                column: "TsvectorTitle")
                .Annotation("Npgsql:IndexMethod", "GIN")
                .Annotation("Npgsql:IndexOperators", new[] { "gin_trgm_ops" });

            migrationBuilder.CreateIndex(
                name: "IX_Playlists_TsvectorTitle",
                table: "Playlists",
                column: "TsvectorTitle")
                .Annotation("Npgsql:IndexMethod", "GIN")
                .Annotation("Npgsql:IndexOperators", new[] { "gin_trgm_ops" });

            migrationBuilder.CreateIndex(
                name: "IX_Albums_TsvectorTitle",
                table: "Albums",
                column: "TsvectorTitle")
                .Annotation("Npgsql:IndexMethod", "GIN")
                .Annotation("Npgsql:IndexOperators", new[] { "gin_trgm_ops" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_TsvectorName",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Tracks_TsvectorTitle",
                table: "Tracks");

            migrationBuilder.DropIndex(
                name: "IX_Playlists_TsvectorTitle",
                table: "Playlists");

            migrationBuilder.DropIndex(
                name: "IX_Albums_TsvectorTitle",
                table: "Albums");

            migrationBuilder.DropColumn(
                name: "TsvectorName",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "TsvectorTitle",
                table: "Tracks");

            migrationBuilder.DropColumn(
                name: "TsvectorTitle",
                table: "Playlists");

            migrationBuilder.DropColumn(
                name: "TsvectorTitle",
                table: "Albums");
        }
    }
}
