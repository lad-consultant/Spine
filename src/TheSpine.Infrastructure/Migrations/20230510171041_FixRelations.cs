using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheSpine.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Segments_LeafNodes_LeafNodeId",
                table: "Segments");

            migrationBuilder.DropIndex(
                name: "IX_Segments_LeafNodeId",
                table: "Segments");

            migrationBuilder.DropColumn(
                name: "LeafNodeId",
                table: "Segments");

            migrationBuilder.AddColumn<int>(
                name: "LeafNodeId",
                table: "SegmentItems",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SegmentItems_LeafNodeId",
                table: "SegmentItems",
                column: "LeafNodeId");

            migrationBuilder.AddForeignKey(
                name: "FK_SegmentItems_LeafNodes_LeafNodeId",
                table: "SegmentItems",
                column: "LeafNodeId",
                principalTable: "LeafNodes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SegmentItems_LeafNodes_LeafNodeId",
                table: "SegmentItems");

            migrationBuilder.DropIndex(
                name: "IX_SegmentItems_LeafNodeId",
                table: "SegmentItems");

            migrationBuilder.DropColumn(
                name: "LeafNodeId",
                table: "SegmentItems");

            migrationBuilder.AddColumn<int>(
                name: "LeafNodeId",
                table: "Segments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Segments_LeafNodeId",
                table: "Segments",
                column: "LeafNodeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Segments_LeafNodes_LeafNodeId",
                table: "Segments",
                column: "LeafNodeId",
                principalTable: "LeafNodes",
                principalColumn: "Id");
        }
    }
}
