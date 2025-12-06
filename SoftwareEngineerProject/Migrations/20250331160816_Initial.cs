using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoftwareEngineerProject.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FacilityTable",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OpenTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    CloseTime = table.Column<TimeOnly>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FacilityTable", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "MembershipTable",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShowerAccess = table.Column<bool>(type: "bit", nullable: false),
                    Tier = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PoolAccessStartTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    PoolAccessEndTime = table.Column<TimeOnly>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MembershipTable", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeTable",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BirthDate = table.Column<DateOnly>(type: "date", nullable: false),
                    StartTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    EndTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    WorkDays = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Teaching = table.Column<bool>(type: "bit", nullable: true),
                    FacilityID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeTable", x => x.ID);
                    table.ForeignKey(
                        name: "FK_EmployeeTable_FacilityTable_FacilityID",
                        column: x => x.FacilityID,
                        principalTable: "FacilityTable",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PoolTable",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FacilityID = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Booked = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PoolTable", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PoolTable_FacilityTable_FacilityID",
                        column: x => x.FacilityID,
                        principalTable: "FacilityTable",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomerTable",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MembershipID = table.Column<int>(type: "int", nullable: true),
                    CheckedIn = table.Column<bool>(type: "bit", nullable: false),
                    AttendanceCount = table.Column<int>(type: "int", nullable: false),
                    PurchasesMade = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerTable", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CustomerTable_MembershipTable_MembershipID",
                        column: x => x.MembershipID,
                        principalTable: "MembershipTable",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "LessonTable",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonCount = table.Column<int>(type: "int", nullable: false),
                    StartTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    EndTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    TeacherID = table.Column<int>(type: "int", nullable: false),
                    PoolID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LessonTable", x => x.ID);
                    table.ForeignKey(
                        name: "FK_LessonTable_EmployeeTable_TeacherID",
                        column: x => x.TeacherID,
                        principalTable: "EmployeeTable",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LessonTable_PoolTable_PoolID",
                        column: x => x.PoolID,
                        principalTable: "PoolTable",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EquipmentTable",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Rented = table.Column<bool>(type: "bit", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FacilityID = table.Column<int>(type: "int", nullable: false),
                    OwnerID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipmentTable", x => x.ID);
                    table.ForeignKey(
                        name: "FK_EquipmentTable_CustomerTable_OwnerID",
                        column: x => x.OwnerID,
                        principalTable: "CustomerTable",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EquipmentTable_FacilityTable_FacilityID",
                        column: x => x.FacilityID,
                        principalTable: "FacilityTable",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LockerTable",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Rented = table.Column<bool>(type: "bit", nullable: false),
                    FacilityID = table.Column<int>(type: "int", nullable: false),
                    OwnerID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LockerTable", x => x.ID);
                    table.ForeignKey(
                        name: "FK_LockerTable_CustomerTable_OwnerID",
                        column: x => x.OwnerID,
                        principalTable: "CustomerTable",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LockerTable_FacilityTable_FacilityID",
                        column: x => x.FacilityID,
                        principalTable: "FacilityTable",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerTable_MembershipID",
                table: "CustomerTable",
                column: "MembershipID");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeTable_FacilityID",
                table: "EmployeeTable",
                column: "FacilityID");

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentTable_FacilityID",
                table: "EquipmentTable",
                column: "FacilityID");

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentTable_OwnerID",
                table: "EquipmentTable",
                column: "OwnerID");

            migrationBuilder.CreateIndex(
                name: "IX_LessonTable_PoolID",
                table: "LessonTable",
                column: "PoolID");

            migrationBuilder.CreateIndex(
                name: "IX_LessonTable_TeacherID",
                table: "LessonTable",
                column: "TeacherID");

            migrationBuilder.CreateIndex(
                name: "IX_LockerTable_FacilityID",
                table: "LockerTable",
                column: "FacilityID");

            migrationBuilder.CreateIndex(
                name: "IX_LockerTable_OwnerID",
                table: "LockerTable",
                column: "OwnerID");

            migrationBuilder.CreateIndex(
                name: "IX_PoolTable_FacilityID",
                table: "PoolTable",
                column: "FacilityID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EquipmentTable");

            migrationBuilder.DropTable(
                name: "LessonTable");

            migrationBuilder.DropTable(
                name: "LockerTable");

            migrationBuilder.DropTable(
                name: "EmployeeTable");

            migrationBuilder.DropTable(
                name: "PoolTable");

            migrationBuilder.DropTable(
                name: "CustomerTable");

            migrationBuilder.DropTable(
                name: "FacilityTable");

            migrationBuilder.DropTable(
                name: "MembershipTable");
        }
    }
}
