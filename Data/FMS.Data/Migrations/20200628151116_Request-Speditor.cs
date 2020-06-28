using Microsoft.EntityFrameworkCore.Migrations;

namespace FMS.Data.Migrations
{
    public partial class RequestSpeditor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SpeditorID",
                table: "Requests",
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

        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
