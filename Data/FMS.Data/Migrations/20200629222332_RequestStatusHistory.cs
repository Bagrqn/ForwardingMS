using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FMS.Data.Migrations
{
    public partial class RequestStatusHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "RequestTypes",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RequestStatusID",
                table: "Requests",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "RequestStatuses",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 30, nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestStatuses", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "requestStatusHistories",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestID = table.Column<int>(nullable: false),
                    OldStatusID = table.Column<int>(nullable: false),
                    NewStatusID = table.Column<int>(nullable: false),
                    DateChange = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_requestStatusHistories", x => x.ID);
                    table.ForeignKey(
                        name: "FK_requestStatusHistories_RequestStatuses_NewStatusID",
                        column: x => x.NewStatusID,
                        principalTable: "RequestStatuses",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_requestStatusHistories_RequestStatuses_OldStatusID",
                        column: x => x.OldStatusID,
                        principalTable: "RequestStatuses",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_requestStatusHistories_Requests_RequestID",
                        column: x => x.RequestID,
                        principalTable: "Requests",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Requests_RequestStatusID",
                table: "Requests",
                column: "RequestStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_requestStatusHistories_NewStatusID",
                table: "requestStatusHistories",
                column: "NewStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_requestStatusHistories_OldStatusID",
                table: "requestStatusHistories",
                column: "OldStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_requestStatusHistories_RequestID",
                table: "requestStatusHistories",
                column: "RequestID");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_RequestStatuses_RequestStatusID",
                table: "Requests",
                column: "RequestStatusID",
                principalTable: "RequestStatuses",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requests_RequestStatuses_RequestStatusID",
                table: "Requests");

            migrationBuilder.DropTable(
                name: "requestStatusHistories");

            migrationBuilder.DropTable(
                name: "RequestStatuses");

            migrationBuilder.DropIndex(
                name: "IX_Requests_RequestStatusID",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "RequestTypes");

            migrationBuilder.DropColumn(
                name: "RequestStatusID",
                table: "Requests");
        }
    }
}
