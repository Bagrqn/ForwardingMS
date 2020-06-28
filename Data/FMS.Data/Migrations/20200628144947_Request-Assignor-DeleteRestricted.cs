using Microsoft.EntityFrameworkCore.Migrations;

namespace FMS.Data.Migrations
{
    public partial class RequestAssignorDeleteRestricted : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Companies_CompanyAssignorID",
                table: "Requests");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Companies_CompanyAssignorID",
                table: "Requests",
                column: "CompanyAssignorID",
                principalTable: "Companies",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Companies_CompanyAssignorID",
                table: "Requests");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Companies_CompanyAssignorID",
                table: "Requests",
                column: "CompanyAssignorID",
                principalTable: "Companies",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
