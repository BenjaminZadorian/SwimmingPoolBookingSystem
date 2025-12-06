using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoftwareEngineerProject.Migrations
{
    /// <inheritdoc />
    public partial class changing_membership_relation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerTable_MembershipTable_MembershipID",
                table: "CustomerTable");

            migrationBuilder.DropIndex(
                name: "IX_CustomerTable_MembershipID",
                table: "CustomerTable");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "MembershipTable",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_CustomerTable_MembershipID",
                table: "CustomerTable",
                column: "MembershipID",
                unique: true,
                filter: "[MembershipID] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerTable_MembershipTable_MembershipID",
                table: "CustomerTable",
                column: "MembershipID",
                principalTable: "MembershipTable",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerTable_MembershipTable_MembershipID",
                table: "CustomerTable");

            migrationBuilder.DropIndex(
                name: "IX_CustomerTable_MembershipID",
                table: "CustomerTable");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "MembershipTable");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerTable_MembershipID",
                table: "CustomerTable",
                column: "MembershipID");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerTable_MembershipTable_MembershipID",
                table: "CustomerTable",
                column: "MembershipID",
                principalTable: "MembershipTable",
                principalColumn: "ID");
        }
    }
}
