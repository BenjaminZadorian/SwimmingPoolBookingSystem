using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoftwareEngineerProject.Migrations
{
    /// <inheritdoc />
    public partial class membership_cust_relationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerTable_MembershipTable_MembershipID",
                table: "CustomerTable");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerTable_MembershipTable_MembershipID",
                table: "CustomerTable",
                column: "MembershipID",
                principalTable: "MembershipTable",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerTable_MembershipTable_MembershipID",
                table: "CustomerTable");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerTable_MembershipTable_MembershipID",
                table: "CustomerTable",
                column: "MembershipID",
                principalTable: "MembershipTable",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
