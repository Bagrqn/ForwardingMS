using Microsoft.EntityFrameworkCore.Migrations;

namespace FMS.Data.Migrations
{
    public partial class RequestAssignorAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CompanyAssignorID",
                table: "Requests",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Requests_CompanyAssignorID",
                table: "Requests",
                column: "CompanyAssignorID");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Companies_CompanyAssignorID",
                table: "Requests",
                column: "CompanyAssignorID",
                principalTable: "Companies",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Companies_CompanyAssignorID",
                table: "Requests");

            migrationBuilder.DropIndex(
                name: "IX_Requests_CompanyAssignorID",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "CompanyAssignorID",
                table: "Requests");
        }
    }
}
