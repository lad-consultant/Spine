using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TheSpine.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpateNodeModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuickLinks_SegmentItems_SegmentItemId",
                table: "QuickLinks");

            migrationBuilder.DropForeignKey(
                name: "FK_SegmentItems_LeafNodes_LeafNodeId",
                table: "SegmentItems");

            migrationBuilder.DropTable(
                name: "LeafNodes");

            migrationBuilder.DropIndex(
                name: "IX_SegmentItems_LeafNodeId",
                table: "SegmentItems");

            migrationBuilder.DropIndex(
                name: "IX_QuickLinks_SegmentItemId",
                table: "QuickLinks");

            migrationBuilder.RenameColumn(
                name: "LeafNodeId",
                table: "SegmentItems",
                newName: "NodeId");

            migrationBuilder.AddColumn<int>(
                name: "ParentId",
                table: "Nodes",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Nodes",
                keyColumn: "Id",
                keyValue: 1,
                column: "ParentId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Nodes",
                keyColumn: "Id",
                keyValue: 2,
                column: "ParentId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Nodes",
                keyColumn: "Id",
                keyValue: 3,
                column: "ParentId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Nodes",
                keyColumn: "Id",
                keyValue: 4,
                column: "ParentId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Nodes",
                keyColumn: "Id",
                keyValue: 5,
                column: "ParentId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Nodes",
                keyColumn: "Id",
                keyValue: 6,
                column: "ParentId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Nodes",
                keyColumn: "Id",
                keyValue: 7,
                column: "ParentId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Nodes",
                keyColumn: "Id",
                keyValue: 8,
                column: "ParentId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Nodes",
                keyColumn: "Id",
                keyValue: 9,
                column: "ParentId",
                value: null);

            migrationBuilder.InsertData(
                table: "Nodes",
                columns: new[] { "Id", "ParentId", "Title" },
                values: new object[,]
                {
                    { 11, 9, "Capture" },
                    { 12, 9, "Create" },
                    { 13, 9, "Plan" },
                    { 14, 9, "Visualize" },
                    { 15, 9, "Compute" },
                    { 16, 9, "Simulate" },
                    { 17, 9, "Analyze" },
                    { 18, 9, "Illustrate" },
                    { 19, 9, "Delineate" },
                    { 20, 9, "Narrate" },
                    { 21, 9, "Specify" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Nodes",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Nodes",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Nodes",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Nodes",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Nodes",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Nodes",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Nodes",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Nodes",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Nodes",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Nodes",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Nodes",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "Nodes");

            migrationBuilder.RenameColumn(
                name: "NodeId",
                table: "SegmentItems",
                newName: "LeafNodeId");

            migrationBuilder.CreateTable(
                name: "LeafNodes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NodeId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeafNodes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LeafNodes_Nodes_NodeId",
                        column: x => x.NodeId,
                        principalTable: "Nodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "LeafNodes",
                columns: new[] { "Id", "NodeId", "Title" },
                values: new object[,]
                {
                    { 11, 9, "Capture" },
                    { 12, 9, "Create" },
                    { 13, 9, "Plan" },
                    { 14, 9, "Visualize" },
                    { 15, 9, "Compute" },
                    { 16, 9, "Simulate" },
                    { 17, 9, "Analyze" },
                    { 18, 9, "Illustrate" },
                    { 19, 9, "Delineate" },
                    { 20, 9, "Narrate" },
                    { 21, 9, "Specify" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_SegmentItems_LeafNodeId",
                table: "SegmentItems",
                column: "LeafNodeId");

            migrationBuilder.CreateIndex(
                name: "IX_QuickLinks_SegmentItemId",
                table: "QuickLinks",
                column: "SegmentItemId");

            migrationBuilder.CreateIndex(
                name: "IX_LeafNodes_NodeId",
                table: "LeafNodes",
                column: "NodeId");

            migrationBuilder.AddForeignKey(
                name: "FK_QuickLinks_SegmentItems_SegmentItemId",
                table: "QuickLinks",
                column: "SegmentItemId",
                principalTable: "SegmentItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SegmentItems_LeafNodes_LeafNodeId",
                table: "SegmentItems",
                column: "LeafNodeId",
                principalTable: "LeafNodes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
