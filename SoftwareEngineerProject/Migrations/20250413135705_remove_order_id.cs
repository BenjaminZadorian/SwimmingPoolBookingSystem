using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoftwareEngineerProject.Migrations
{
    /// <inheritdoc />
    public partial class remove_order_id : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderID",
                table: "OrderTable");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderID",
                table: "OrderTable",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
