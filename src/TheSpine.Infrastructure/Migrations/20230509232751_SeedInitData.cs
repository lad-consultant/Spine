using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TheSpine.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedInitData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "LeafNodes",
                columns: new[] { "Id", "NodeId", "ParentNodeId", "Title" },
                values: new object[,]
                {
                    { 11, null, 9, "Capture" },
                    { 12, null, 9, "Create" },
                    { 13, null, 9, "Plan" },
                    { 14, null, 9, "Visualize" },
                    { 15, null, 9, "Compute" },
                    { 16, null, 9, "Simulate" },
                    { 17, null, 9, "Analyze" },
                    { 18, null, 9, "Illustrate" },
                    { 19, null, 9, "Delineate" },
                    { 20, null, 9, "Narrate" },
                    { 21, null, 9, "Specify" }
                });

            migrationBuilder.InsertData(
                table: "Nodes",
                columns: new[] { "Id", "Title" },
                values: new object[,]
                {
                    { 1, "Pursue" },
                    { 2, "Secure" },
                    { 3, "Visualize" },
                    { 4, "Exchange" },
                    { 5, "Resolve" },
                    { 6, "Administer" },
                    { 7, "Market" },
                    { 8, "Manage" },
                    { 9, "Design" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "LeafNodes",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "LeafNodes",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "LeafNodes",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "LeafNodes",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "LeafNodes",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "LeafNodes",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "LeafNodes",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "LeafNodes",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "LeafNodes",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "LeafNodes",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "LeafNodes",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Nodes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Nodes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Nodes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Nodes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Nodes",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Nodes",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Nodes",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Nodes",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Nodes",
                keyColumn: "Id",
                keyValue: 9);
        }
    }
}
