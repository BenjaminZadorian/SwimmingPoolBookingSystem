using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoftwareEngineerProject.Migrations
{
    /// <inheritdoc />
    public partial class facility_fix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EquipmentTable_FacilityTable_FacilityID",
                table: "EquipmentTable");

            migrationBuilder.AlterColumn<int>(
                name: "FacilityID",
                table: "EquipmentTable",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_EquipmentTable_FacilityTable_FacilityID",
                table: "EquipmentTable",
                column: "FacilityID",
                principalTable: "FacilityTable",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EquipmentTable_FacilityTable_FacilityID",
                table: "EquipmentTable");

            migrationBuilder.AlterColumn<int>(
                name: "FacilityID",
                table: "EquipmentTable",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_EquipmentTable_FacilityTable_FacilityID",
                table: "EquipmentTable",
                column: "FacilityID",
                principalTable: "FacilityTable",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
