using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoftwareEngineerProject.Migrations
{
    /// <inheritdoc />
    public partial class make_facility_nullabe : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeTable_FacilityTable_FacilityID",
                table: "EmployeeTable");

            migrationBuilder.AlterColumn<int>(
                name: "FacilityID",
                table: "EmployeeTable",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeTable_FacilityTable_FacilityID",
                table: "EmployeeTable",
                column: "FacilityID",
                principalTable: "FacilityTable",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeTable_FacilityTable_FacilityID",
                table: "EmployeeTable");

            migrationBuilder.AlterColumn<int>(
                name: "FacilityID",
                table: "EmployeeTable",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeTable_FacilityTable_FacilityID",
                table: "EmployeeTable",
                column: "FacilityID",
                principalTable: "FacilityTable",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
