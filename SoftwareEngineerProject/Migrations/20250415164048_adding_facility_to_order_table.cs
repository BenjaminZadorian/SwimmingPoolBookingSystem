using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoftwareEngineerProject.Migrations
{
    /// <inheritdoc />
    public partial class adding_facility_to_order_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FacilityId",
                table: "OrderTable",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderTable_FacilityId",
                table: "OrderTable",
                column: "FacilityId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderTable_FacilityTable_FacilityId",
                table: "OrderTable",
                column: "FacilityId",
                principalTable: "FacilityTable",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderTable_FacilityTable_FacilityId",
                table: "OrderTable");

            migrationBuilder.DropIndex(
                name: "IX_OrderTable_FacilityId",
                table: "OrderTable");

            migrationBuilder.DropColumn(
                name: "FacilityId",
                table: "OrderTable");
        }
    }
}
