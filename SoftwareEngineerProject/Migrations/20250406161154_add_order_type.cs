using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoftwareEngineerProject.Migrations
{
    /// <inheritdoc />
    public partial class add_order_type : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderTypeID",
                table: "OrderTable",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_OrderTable_OrderTypeID",
                table: "OrderTable",
                column: "OrderTypeID");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderTable_MerchandiseTable_OrderTypeID",
                table: "OrderTable",
                column: "OrderTypeID",
                principalTable: "MerchandiseTable",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderTable_MerchandiseTable_OrderTypeID",
                table: "OrderTable");

            migrationBuilder.DropIndex(
                name: "IX_OrderTable_OrderTypeID",
                table: "OrderTable");

            migrationBuilder.DropColumn(
                name: "OrderTypeID",
                table: "OrderTable");
        }
    }
}
