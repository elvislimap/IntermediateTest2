using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IntermediateTest2.Service.Api.Migrations
{
    public partial class IntermediateTest2Migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "IntermediateTest2");

            migrationBuilder.CreateTable(
                name: "Employees",
                schema: "IntermediateTest2",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    MonthlySalary = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmployeeId);
                });

            migrationBuilder.CreateTable(
                name: "InflationAdjusts",
                schema: "IntermediateTest2",
                columns: table => new
                {
                    InflationAdjustId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Percentage = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    AdjustmentDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InflationAdjusts", x => x.InflationAdjustId);
                });

            migrationBuilder.CreateTable(
                name: "SharedFunds",
                schema: "IntermediateTest2",
                columns: table => new
                {
                    SharedFundId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    ContributionDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SharedFunds", x => x.SharedFundId);
                    table.ForeignKey(
                        name: "FK_SharedFunds_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "IntermediateTest2",
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SharedFunds_EmployeeId",
                schema: "IntermediateTest2",
                table: "SharedFunds",
                column: "EmployeeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InflationAdjusts",
                schema: "IntermediateTest2");

            migrationBuilder.DropTable(
                name: "SharedFunds",
                schema: "IntermediateTest2");

            migrationBuilder.DropTable(
                name: "Employees",
                schema: "IntermediateTest2");
        }
    }
}
