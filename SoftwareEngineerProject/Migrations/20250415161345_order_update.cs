using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoftwareEngineerProject.Migrations
{
    /// <inheritdoc />
    public partial class order_update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MerchandiseTable_FacilityTable_LocationID",
                table: "MerchandiseTable");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderTable_CustomerTable_CustomerID",
                table: "OrderTable");

            migrationBuilder.DropColumn(
                name: "OrderType",
                table: "OrderTable");

            migrationBuilder.RenameColumn(
                name: "CustomerID",
                table: "OrderTable",
                newName: "CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderTable_CustomerID",
                table: "OrderTable",
                newName: "IX_OrderTable_CustomerId");

            migrationBuilder.RenameColumn(
                name: "LocationID",
                table: "MerchandiseTable",
                newName: "FacilityId");

            migrationBuilder.RenameIndex(
                name: "IX_MerchandiseTable_LocationID",
                table: "MerchandiseTable",
                newName: "IX_MerchandiseTable_FacilityId");

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
                name: "FK_MerchandiseTable_FacilityTable_FacilityId",
                table: "MerchandiseTable",
                column: "FacilityId",
                principalTable: "FacilityTable",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderTable_CustomerTable_CustomerId",
                table: "OrderTable",
                column: "CustomerId",
                principalTable: "CustomerTable",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderTable_MerchandiseTable_OrderTypeId",
                table: "OrderTable",
                column: "OrderTypeId",
                principalTable: "MerchandiseTable",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MerchandiseTable_FacilityTable_FacilityId",
                table: "MerchandiseTable");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderTable_CustomerTable_CustomerId",
                table: "OrderTable");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderTable_MerchandiseTable_OrderTypeId",
                table: "OrderTable");

            migrationBuilder.DropIndex(
                name: "IX_OrderTable_OrderTypeId",
                table: "OrderTable");

            migrationBuilder.DropColumn(
                name: "OrderTypeId",
                table: "OrderTable");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "OrderTable",
                newName: "CustomerID");

            migrationBuilder.RenameIndex(
                name: "IX_OrderTable_CustomerId",
                table: "OrderTable",
                newName: "IX_OrderTable_CustomerID");

            migrationBuilder.RenameColumn(
                name: "FacilityId",
                table: "MerchandiseTable",
                newName: "LocationID");

            migrationBuilder.RenameIndex(
                name: "IX_MerchandiseTable_FacilityId",
                table: "MerchandiseTable",
                newName: "IX_MerchandiseTable_LocationID");

            migrationBuilder.AddColumn<string>(
                name: "OrderType",
                table: "OrderTable",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_MerchandiseTable_FacilityTable_LocationID",
                table: "MerchandiseTable",
                column: "LocationID",
                principalTable: "FacilityTable",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderTable_CustomerTable_CustomerID",
                table: "OrderTable",
                column: "CustomerID",
                principalTable: "CustomerTable",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
