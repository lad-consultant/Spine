using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TheSpine.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedingTheSegments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Segments",
                columns: new[] { "Id", "Title" },
                values: new object[,]
                {
                    { 31, "Rendering/Animation" },
                    { 32, "Reports/Postproduction" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Segments",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "Segments",
                keyColumn: "Id",
                keyValue: 32);
        }
    }
}
