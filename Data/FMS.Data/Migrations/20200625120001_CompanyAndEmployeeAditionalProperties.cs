using Microsoft.EntityFrameworkCore.Migrations;

namespace FMS.Data.Migrations
{
    public partial class CompanyAndEmployeeAditionalProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CompanyBoolProp",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 30, nullable: false),
                    Value = table.Column<bool>(nullable: false),
                    CompanyId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyBoolProp", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyBoolProp_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CompanyNumericProp",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 30, nullable: false),
                    Value = table.Column<double>(nullable: false),
                    CompanyId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyNumericProp", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyNumericProp_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CompanyStringProp",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 30, nullable: false),
                    Value = table.Column<string>(nullable: true),
                    CompanyId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyStringProp", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyStringProp_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeBoolProp",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 30, nullable: false),
                    Value = table.Column<bool>(nullable: false),
                    EmployeeID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeBoolProp", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeBoolProp_Employees_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeNumericProp",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 30, nullable: false),
                    Value = table.Column<double>(nullable: false),
                    EmployeeID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeNumericProp", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeNumericProp_Employees_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeStringProps",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 30, nullable: false),
                    Value = table.Column<string>(nullable: true),
                    EmployeeID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeStringProps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeStringProps_Employees_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompanyBoolProp_CompanyId",
                table: "CompanyBoolProp",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyNumericProp_CompanyId",
                table: "CompanyNumericProp",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyStringProp_CompanyId",
                table: "CompanyStringProp",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeBoolProp_EmployeeID",
                table: "EmployeeBoolProp",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeNumericProp_EmployeeID",
                table: "EmployeeNumericProp",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeStringProps_EmployeeID",
                table: "EmployeeStringProps",
                column: "EmployeeID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompanyBoolProp");

            migrationBuilder.DropTable(
                name: "CompanyNumericProp");

            migrationBuilder.DropTable(
                name: "CompanyStringProp");

            migrationBuilder.DropTable(
                name: "EmployeeBoolProp");

            migrationBuilder.DropTable(
                name: "EmployeeNumericProp");

            migrationBuilder.DropTable(
                name: "EmployeeStringProps");
        }
    }
}
