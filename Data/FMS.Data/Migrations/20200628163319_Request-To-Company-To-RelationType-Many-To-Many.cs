using Microsoft.EntityFrameworkCore.Migrations;

namespace FMS.Data.Migrations
{
    public partial class RequestToCompanyToRelationTypeManyToMany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Companies_CompanyAssignorID",
                table: "Requests");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Companies_CompanyPayerID",
                table: "Requests");

            migrationBuilder.DropIndex(
                name: "IX_Requests_CompanyAssignorID",
                table: "Requests");

            migrationBuilder.DropIndex(
                name: "IX_Requests_CompanyPayerID",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "CompanyAssignorID",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "CompanyPayerID",
                table: "Requests");

            migrationBuilder.CreateTable(
                name: "RequestToCompanyRelationTypes",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 30, nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestToCompanyRelationTypes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "RequestToCompanies",
                columns: table => new
                {
                    RequestID = table.Column<int>(nullable: false),
                    CompanyID = table.Column<int>(nullable: false),
                    RequestToCompanyRelationTypeID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestToCompanies", x => new { x.RequestID, x.CompanyID });
                    table.ForeignKey(
                        name: "FK_RequestToCompanies_Companies_CompanyID",
                        column: x => x.CompanyID,
                        principalTable: "Companies",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RequestToCompanies_Requests_RequestID",
                        column: x => x.RequestID,
                        principalTable: "Requests",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RequestToCompanies_RequestToCompanyRelationTypes_RequestToCompanyRelationTypeID",
                        column: x => x.RequestToCompanyRelationTypeID,
                        principalTable: "RequestToCompanyRelationTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RequestToCompanies_CompanyID",
                table: "RequestToCompanies",
                column: "CompanyID");

            migrationBuilder.CreateIndex(
                name: "IX_RequestToCompanies_RequestToCompanyRelationTypeID",
                table: "RequestToCompanies",
                column: "RequestToCompanyRelationTypeID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RequestToCompanies");

            migrationBuilder.DropTable(
                name: "RequestToCompanyRelationTypes");

            migrationBuilder.AddColumn<int>(
                name: "CompanyAssignorID",
                table: "Requests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CompanyPayerID",
                table: "Requests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Requests_CompanyAssignorID",
                table: "Requests",
                column: "CompanyAssignorID");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_CompanyPayerID",
                table: "Requests",
                column: "CompanyPayerID");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Companies_CompanyAssignorID",
                table: "Requests",
                column: "CompanyAssignorID",
                principalTable: "Companies",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Companies_CompanyPayerID",
                table: "Requests",
                column: "CompanyPayerID",
                principalTable: "Companies",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
