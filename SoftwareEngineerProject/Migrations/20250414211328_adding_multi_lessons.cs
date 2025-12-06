using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoftwareEngineerProject.Migrations
{
    /// <inheritdoc />
    public partial class adding_multi_lessons : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_LessonTable_PoolID",
                table: "LessonTable");

            migrationBuilder.CreateIndex(
                name: "IX_LessonTable_PoolID",
                table: "LessonTable",
                column: "PoolID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_LessonTable_PoolID",
                table: "LessonTable");

            migrationBuilder.CreateIndex(
                name: "IX_LessonTable_PoolID",
                table: "LessonTable",
                column: "PoolID",
                unique: true);
        }
    }
}
