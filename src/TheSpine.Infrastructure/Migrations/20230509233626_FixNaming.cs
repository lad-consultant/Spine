using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheSpine.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixNaming : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LeafNodes_Nodes_NodeId",
                table: "LeafNodes");

            migrationBuilder.DropColumn(
                name: "ParentNodeId",
                table: "LeafNodes");

            migrationBuilder.AlterColumn<int>(
                name: "NodeId",
                table: "LeafNodes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "LeafNodes",
                keyColumn: "Id",
                keyValue: 11,
                column: "NodeId",
                value: 9);

            migrationBuilder.UpdateData(
                table: "LeafNodes",
                keyColumn: "Id",
                keyValue: 12,
                column: "NodeId",
                value: 9);

            migrationBuilder.UpdateData(
                table: "LeafNodes",
                keyColumn: "Id",
                keyValue: 13,
                column: "NodeId",
                value: 9);

            migrationBuilder.UpdateData(
                table: "LeafNodes",
                keyColumn: "Id",
                keyValue: 14,
                column: "NodeId",
                value: 9);

            migrationBuilder.UpdateData(
                table: "LeafNodes",
                keyColumn: "Id",
                keyValue: 15,
                column: "NodeId",
                value: 9);

            migrationBuilder.UpdateData(
                table: "LeafNodes",
                keyColumn: "Id",
                keyValue: 16,
                column: "NodeId",
                value: 9);

            migrationBuilder.UpdateData(
                table: "LeafNodes",
                keyColumn: "Id",
                keyValue: 17,
                column: "NodeId",
                value: 9);

            migrationBuilder.UpdateData(
                table: "LeafNodes",
                keyColumn: "Id",
                keyValue: 18,
                column: "NodeId",
                value: 9);

            migrationBuilder.UpdateData(
                table: "LeafNodes",
                keyColumn: "Id",
                keyValue: 19,
                column: "NodeId",
                value: 9);

            migrationBuilder.UpdateData(
                table: "LeafNodes",
                keyColumn: "Id",
                keyValue: 20,
                column: "NodeId",
                value: 9);

            migrationBuilder.UpdateData(
                table: "LeafNodes",
                keyColumn: "Id",
                keyValue: 21,
                column: "NodeId",
                value: 9);

            migrationBuilder.AddForeignKey(
                name: "FK_LeafNodes_Nodes_NodeId",
                table: "LeafNodes",
                column: "NodeId",
                principalTable: "Nodes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LeafNodes_Nodes_NodeId",
                table: "LeafNodes");

            migrationBuilder.AlterColumn<int>(
                name: "NodeId",
                table: "LeafNodes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ParentNodeId",
                table: "LeafNodes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "LeafNodes",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "NodeId", "ParentNodeId" },
                values: new object[] { null, 9 });

            migrationBuilder.UpdateData(
                table: "LeafNodes",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "NodeId", "ParentNodeId" },
                values: new object[] { null, 9 });

            migrationBuilder.UpdateData(
                table: "LeafNodes",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "NodeId", "ParentNodeId" },
                values: new object[] { null, 9 });

            migrationBuilder.UpdateData(
                table: "LeafNodes",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "NodeId", "ParentNodeId" },
                values: new object[] { null, 9 });

            migrationBuilder.UpdateData(
                table: "LeafNodes",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "NodeId", "ParentNodeId" },
                values: new object[] { null, 9 });

            migrationBuilder.UpdateData(
                table: "LeafNodes",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "NodeId", "ParentNodeId" },
                values: new object[] { null, 9 });

            migrationBuilder.UpdateData(
                table: "LeafNodes",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "NodeId", "ParentNodeId" },
                values: new object[] { null, 9 });

            migrationBuilder.UpdateData(
                table: "LeafNodes",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "NodeId", "ParentNodeId" },
                values: new object[] { null, 9 });

            migrationBuilder.UpdateData(
                table: "LeafNodes",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "NodeId", "ParentNodeId" },
                values: new object[] { null, 9 });

            migrationBuilder.UpdateData(
                table: "LeafNodes",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "NodeId", "ParentNodeId" },
                values: new object[] { null, 9 });

            migrationBuilder.UpdateData(
                table: "LeafNodes",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "NodeId", "ParentNodeId" },
                values: new object[] { null, 9 });

            migrationBuilder.AddForeignKey(
                name: "FK_LeafNodes_Nodes_NodeId",
                table: "LeafNodes",
                column: "NodeId",
                principalTable: "Nodes",
                principalColumn: "Id");
        }
    }
}
