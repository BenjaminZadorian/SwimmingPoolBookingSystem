using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoftwareEngineerProject.Migrations
{
    /// <inheritdoc />
    public partial class lesson_fk_fix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EquipmentTable_CustomerTable_OwnerID",
                table: "EquipmentTable");

            migrationBuilder.DropIndex(
                name: "IX_LessonTable_PoolID",
                table: "LessonTable");

            migrationBuilder.CreateIndex(
                name: "IX_LessonTable_PoolID",
                table: "LessonTable",
                column: "PoolID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_EquipmentTable_CustomerTable_OwnerID",
                table: "EquipmentTable",
                column: "OwnerID",
                principalTable: "CustomerTable",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EquipmentTable_CustomerTable_OwnerID",
                table: "EquipmentTable");

            migrationBuilder.DropIndex(
                name: "IX_LessonTable_PoolID",
                table: "LessonTable");

            migrationBuilder.CreateIndex(
                name: "IX_LessonTable_PoolID",
                table: "LessonTable",
                column: "PoolID");

            migrationBuilder.AddForeignKey(
                name: "FK_EquipmentTable_CustomerTable_OwnerID",
                table: "EquipmentTable",
                column: "OwnerID",
                principalTable: "CustomerTable",
                principalColumn: "ID");
        }
    }
}
