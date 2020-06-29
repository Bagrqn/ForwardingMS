using Microsoft.EntityFrameworkCore.Migrations;

namespace FMS.Data.Migrations
{
    public partial class RequestToEmployeeToRelationTypeManyToMany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Employees_SpeditorID",
                table: "Requests");

            migrationBuilder.DropIndex(
                name: "IX_Requests_SpeditorID",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "SpeditorID",
                table: "Requests");

            migrationBuilder.CreateTable(
                name: "RequestToEmployeeRelationTypes",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 30, nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestToEmployeeRelationTypes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "RequestToEmployees",
                columns: table => new
                {
                    RequestID = table.Column<int>(nullable: false),
                    EmployeeID = table.Column<int>(nullable: false),
                    RequestToEmployeeRelationTypeID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestToEmployees", x => new { x.RequestID, x.EmployeeID });
                    table.ForeignKey(
                        name: "FK_RequestToEmployees_Employees_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "Employees",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RequestToEmployees_Requests_RequestID",
                        column: x => x.RequestID,
                        principalTable: "Requests",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RequestToEmployees_RequestToEmployeeRelationTypes_RequestToEmployeeRelationTypeID",
                        column: x => x.RequestToEmployeeRelationTypeID,
                        principalTable: "RequestToEmployeeRelationTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RequestToEmployees_EmployeeID",
                table: "RequestToEmployees",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_RequestToEmployees_RequestToEmployeeRelationTypeID",
                table: "RequestToEmployees",
                column: "RequestToEmployeeRelationTypeID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RequestToEmployees");

            migrationBuilder.DropTable(
                name: "RequestToEmployeeRelationTypes");

            migrationBuilder.AddColumn<int>(
                name: "SpeditorID",
                table: "Requests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Requests_SpeditorID",
                table: "Requests",
                column: "SpeditorID");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Employees_SpeditorID",
                table: "Requests",
                column: "SpeditorID",
                principalTable: "Employees",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
