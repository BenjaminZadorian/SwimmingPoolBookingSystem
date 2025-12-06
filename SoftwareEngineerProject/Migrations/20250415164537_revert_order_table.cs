using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoftwareEngineerProject.Migrations
{
    /// <inheritdoc />
    public partial class revert_order_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderTable_MerchandiseTable_OrderTypeId",
                table: "OrderTable");

            migrationBuilder.DropIndex(
                name: "IX_OrderTable_OrderTypeId",
                table: "OrderTable");

            migrationBuilder.DropColumn(
                name: "OrderTypeId",
                table: "OrderTable");

            migrationBuilder.AddColumn<string>(
                name: "OrderType",
                table: "OrderTable",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderType",
                table: "OrderTable");

            migrationBuilder.AddColumn<int>(
                name: "OrderTypeId",
                table: "OrderTable",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderTable_OrderTypeId",
                table: "OrderTable",
                column: "OrderTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderTable_MerchandiseTable_OrderTypeId",
                table: "OrderTable",
                column: "OrderTypeId",
                principalTable: "MerchandiseTable",
                principalColumn: "ID");
        }
    }
}
