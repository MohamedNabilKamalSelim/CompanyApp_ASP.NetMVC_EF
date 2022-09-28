using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyApp.Migrations
{
    public partial class init_mig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "HR");

            migrationBuilder.CreateTable(
                name: "Department",
                schema: "HR",
                columns: table => new
                {
                    DepartmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmentName = table.Column<string>(type: "varchar(150)", nullable: false),
                    DepartmentAbbreviation = table.Column<string>(type: "varchar(7)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Department", x => x.DepartmentId);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                schema: "HR",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeName = table.Column<string>(type: "varchar(200)", nullable: false),
                    EmployeeImageUrl = table.Column<string>(type: "varchar(250)", nullable: true),
                    DOB = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Salary = table.Column<decimal>(type: "decimal(12,2)", nullable: false),
                    HiringDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NationalId = table.Column<string>(type: "varchar(14)", maxLength: 14, nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmployeeId);
                    table.ForeignKey(
                        name: "FK_Employees_Department_DepartmentId",
                        column: x => x.DepartmentId,
                        principalSchema: "HR",
                        principalTable: "Department",
                        principalColumn: "DepartmentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_DepartmentId",
                schema: "HR",
                table: "Employees",
                column: "DepartmentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees",
                schema: "HR");

            migrationBuilder.DropTable(
                name: "Department",
                schema: "HR");
        }
    }
}
