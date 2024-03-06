using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheSpine.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class OrderingIndexAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderingIndex",
                table: "Nodes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Nodes",
                keyColumn: "Id",
                keyValue: 1,
                column: "OrderingIndex",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Nodes",
                keyColumn: "Id",
                keyValue: 2,
                column: "OrderingIndex",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Nodes",
                keyColumn: "Id",
                keyValue: 3,
                column: "OrderingIndex",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Nodes",
                keyColumn: "Id",
                keyValue: 4,
                column: "OrderingIndex",
                value: 3);

            migrationBuilder.UpdateData(
                table: "Nodes",
                keyColumn: "Id",
                keyValue: 5,
                column: "OrderingIndex",
                value: 4);

            migrationBuilder.UpdateData(
                table: "Nodes",
                keyColumn: "Id",
                keyValue: 6,
                column: "OrderingIndex",
                value: 5);

            migrationBuilder.UpdateData(
                table: "Nodes",
                keyColumn: "Id",
                keyValue: 7,
                column: "OrderingIndex",
                value: 6);

            migrationBuilder.UpdateData(
                table: "Nodes",
                keyColumn: "Id",
                keyValue: 8,
                column: "OrderingIndex",
                value: 7);

            migrationBuilder.UpdateData(
                table: "Nodes",
                keyColumn: "Id",
                keyValue: 9,
                column: "OrderingIndex",
                value: 8);

            migrationBuilder.UpdateData(
                table: "Nodes",
                keyColumn: "Id",
                keyValue: 11,
                column: "OrderingIndex",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Nodes",
                keyColumn: "Id",
                keyValue: 12,
                column: "OrderingIndex",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Nodes",
                keyColumn: "Id",
                keyValue: 13,
                column: "OrderingIndex",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Nodes",
                keyColumn: "Id",
                keyValue: 14,
                column: "OrderingIndex",
                value: 3);

            migrationBuilder.UpdateData(
                table: "Nodes",
                keyColumn: "Id",
                keyValue: 15,
                column: "OrderingIndex",
                value: 4);

            migrationBuilder.UpdateData(
                table: "Nodes",
                keyColumn: "Id",
                keyValue: 16,
                column: "OrderingIndex",
                value: 5);

            migrationBuilder.UpdateData(
                table: "Nodes",
                keyColumn: "Id",
                keyValue: 17,
                column: "OrderingIndex",
                value: 6);

            migrationBuilder.UpdateData(
                table: "Nodes",
                keyColumn: "Id",
                keyValue: 18,
                column: "OrderingIndex",
                value: 7);

            migrationBuilder.UpdateData(
                table: "Nodes",
                keyColumn: "Id",
                keyValue: 19,
                column: "OrderingIndex",
                value: 8);

            migrationBuilder.UpdateData(
                table: "Nodes",
                keyColumn: "Id",
                keyValue: 20,
                column: "OrderingIndex",
                value: 9);

            migrationBuilder.UpdateData(
                table: "Nodes",
                keyColumn: "Id",
                keyValue: 21,
                column: "OrderingIndex",
                value: 10);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderingIndex",
                table: "Nodes");
        }
    }
}
