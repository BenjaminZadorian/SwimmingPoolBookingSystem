using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoftwareEngineerProject.Migrations
{
    /// <inheritdoc />
    public partial class adding_customer_leson : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CustomerLessonTable",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerID = table.Column<int>(type: "int", nullable: false),
                    LessonID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerLessonTable", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CustomerLessonTable_CustomerTable_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "CustomerTable",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomerLessonTable_LessonTable_LessonID",
                        column: x => x.LessonID,
                        principalTable: "LessonTable",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerLessonTable_CustomerID",
                table: "CustomerLessonTable",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerLessonTable_LessonID",
                table: "CustomerLessonTable",
                column: "LessonID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerLessonTable");
        }
    }
}
