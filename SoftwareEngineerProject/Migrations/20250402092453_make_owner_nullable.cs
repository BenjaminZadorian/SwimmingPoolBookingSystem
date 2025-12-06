using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoftwareEngineerProject.Migrations
{
    /// <inheritdoc />
    public partial class make_owner_nullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EquipmentTable_CustomerTable_OwnerID",
                table: "EquipmentTable");

            migrationBuilder.AlterColumn<int>(
                name: "OwnerID",
                table: "EquipmentTable",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_EquipmentTable_CustomerTable_OwnerID",
                table: "EquipmentTable",
                column: "OwnerID",
                principalTable: "CustomerTable",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EquipmentTable_CustomerTable_OwnerID",
                table: "EquipmentTable");

            migrationBuilder.AlterColumn<int>(
                name: "OwnerID",
                table: "EquipmentTable",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_EquipmentTable_CustomerTable_OwnerID",
                table: "EquipmentTable",
                column: "OwnerID",
                principalTable: "CustomerTable",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
