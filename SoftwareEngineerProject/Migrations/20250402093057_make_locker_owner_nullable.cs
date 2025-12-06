using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoftwareEngineerProject.Migrations
{
    /// <inheritdoc />
    public partial class make_locker_owner_nullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LockerTable_CustomerTable_OwnerID",
                table: "LockerTable");

            migrationBuilder.AlterColumn<int>(
                name: "OwnerID",
                table: "LockerTable",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_LockerTable_CustomerTable_OwnerID",
                table: "LockerTable",
                column: "OwnerID",
                principalTable: "CustomerTable",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LockerTable_CustomerTable_OwnerID",
                table: "LockerTable");

            migrationBuilder.AlterColumn<int>(
                name: "OwnerID",
                table: "LockerTable",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_LockerTable_CustomerTable_OwnerID",
                table: "LockerTable",
                column: "OwnerID",
                principalTable: "CustomerTable",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
