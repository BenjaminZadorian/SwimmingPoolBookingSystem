using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoftwareEngineerProject.Migrations
{
    /// <inheritdoc />
    public partial class lesson_pool_fk_fix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LessonTable_PoolTable_PoolID",
                table: "LessonTable");

            migrationBuilder.DropForeignKey(
                name: "FK_PoolTable_FacilityTable_FacilityID",
                table: "PoolTable");

            migrationBuilder.AddForeignKey(
                name: "FK_LessonTable_PoolTable_PoolID",
                table: "LessonTable",
                column: "PoolID",
                principalTable: "PoolTable",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PoolTable_FacilityTable_FacilityID",
                table: "PoolTable",
                column: "FacilityID",
                principalTable: "FacilityTable",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LessonTable_PoolTable_PoolID",
                table: "LessonTable");

            migrationBuilder.DropForeignKey(
                name: "FK_PoolTable_FacilityTable_FacilityID",
                table: "PoolTable");

            migrationBuilder.AddForeignKey(
                name: "FK_LessonTable_PoolTable_PoolID",
                table: "LessonTable",
                column: "PoolID",
                principalTable: "PoolTable",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PoolTable_FacilityTable_FacilityID",
                table: "PoolTable",
                column: "FacilityID",
                principalTable: "FacilityTable",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
