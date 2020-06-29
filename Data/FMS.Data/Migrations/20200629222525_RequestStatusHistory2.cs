using Microsoft.EntityFrameworkCore.Migrations;

namespace FMS.Data.Migrations
{
    public partial class RequestStatusHistory2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_requestStatusHistories_RequestStatuses_NewStatusID",
                table: "requestStatusHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_requestStatusHistories_RequestStatuses_OldStatusID",
                table: "requestStatusHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_requestStatusHistories_Requests_RequestID",
                table: "requestStatusHistories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_requestStatusHistories",
                table: "requestStatusHistories");

            migrationBuilder.RenameTable(
                name: "requestStatusHistories",
                newName: "RequestStatusHistories");

            migrationBuilder.RenameIndex(
                name: "IX_requestStatusHistories_RequestID",
                table: "RequestStatusHistories",
                newName: "IX_RequestStatusHistories_RequestID");

            migrationBuilder.RenameIndex(
                name: "IX_requestStatusHistories_OldStatusID",
                table: "RequestStatusHistories",
                newName: "IX_RequestStatusHistories_OldStatusID");

            migrationBuilder.RenameIndex(
                name: "IX_requestStatusHistories_NewStatusID",
                table: "RequestStatusHistories",
                newName: "IX_RequestStatusHistories_NewStatusID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RequestStatusHistories",
                table: "RequestStatusHistories",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_RequestStatusHistories_RequestStatuses_NewStatusID",
                table: "RequestStatusHistories",
                column: "NewStatusID",
                principalTable: "RequestStatuses",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RequestStatusHistories_RequestStatuses_OldStatusID",
                table: "RequestStatusHistories",
                column: "OldStatusID",
                principalTable: "RequestStatuses",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RequestStatusHistories_Requests_RequestID",
                table: "RequestStatusHistories",
                column: "RequestID",
                principalTable: "Requests",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RequestStatusHistories_RequestStatuses_NewStatusID",
                table: "RequestStatusHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_RequestStatusHistories_RequestStatuses_OldStatusID",
                table: "RequestStatusHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_RequestStatusHistories_Requests_RequestID",
                table: "RequestStatusHistories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RequestStatusHistories",
                table: "RequestStatusHistories");

            migrationBuilder.RenameTable(
                name: "RequestStatusHistories",
                newName: "requestStatusHistories");

            migrationBuilder.RenameIndex(
                name: "IX_RequestStatusHistories_RequestID",
                table: "requestStatusHistories",
                newName: "IX_requestStatusHistories_RequestID");

            migrationBuilder.RenameIndex(
                name: "IX_RequestStatusHistories_OldStatusID",
                table: "requestStatusHistories",
                newName: "IX_requestStatusHistories_OldStatusID");

            migrationBuilder.RenameIndex(
                name: "IX_RequestStatusHistories_NewStatusID",
                table: "requestStatusHistories",
                newName: "IX_requestStatusHistories_NewStatusID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_requestStatusHistories",
                table: "requestStatusHistories",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_requestStatusHistories_RequestStatuses_NewStatusID",
                table: "requestStatusHistories",
                column: "NewStatusID",
                principalTable: "RequestStatuses",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_requestStatusHistories_RequestStatuses_OldStatusID",
                table: "requestStatusHistories",
                column: "OldStatusID",
                principalTable: "RequestStatuses",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_requestStatusHistories_Requests_RequestID",
                table: "requestStatusHistories",
                column: "RequestID",
                principalTable: "Requests",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
