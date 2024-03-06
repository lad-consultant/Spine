using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheSpine.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RelationFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SegmentItems_Segments_SegmentId",
                table: "SegmentItems");

            migrationBuilder.DropIndex(
                name: "IX_SegmentItems_SegmentId",
                table: "SegmentItems");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_SegmentItems_SegmentId",
                table: "SegmentItems",
                column: "SegmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_SegmentItems_Segments_SegmentId",
                table: "SegmentItems",
                column: "SegmentId",
                principalTable: "Segments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
