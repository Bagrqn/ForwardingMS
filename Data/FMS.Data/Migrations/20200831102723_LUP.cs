using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FMS.Data.Migrations
{
    public partial class LUP : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BirhtDate",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Addres",
                table: "Companies");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreate",
                table: "Requests",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "BirthDate",
                table: "Employees",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Companies",
                maxLength: 120,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PackageTypes",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeName = table.Column<string>(maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PackageTypes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Postcodes",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(nullable: true),
                    CityID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Postcodes", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Postcodes_Cities_CityID",
                        column: x => x.CityID,
                        principalTable: "Cities",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Loads",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 30, nullable: false),
                    Comment = table.Column<string>(nullable: true),
                    PackageTypeID = table.Column<int>(nullable: false),
                    PackageCount = table.Column<int>(nullable: false),
                    RequestID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Loads", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Loads_PackageTypes_PackageTypeID",
                        column: x => x.PackageTypeID,
                        principalTable: "PackageTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Loads_Requests_RequestID",
                        column: x => x.RequestID,
                        principalTable: "Requests",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LoadingUnloadingPoints",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    SenderRecieverID = table.Column<int>(nullable: false),
                    CityID = table.Column<int>(nullable: false),
                    PostcodeID = table.Column<int>(nullable: false),
                    Address = table.Column<string>(maxLength: 100, nullable: false),
                    RequestID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoadingUnloadingPoints", x => x.ID);
                    table.ForeignKey(
                        name: "FK_LoadingUnloadingPoints_Cities_CityID",
                        column: x => x.CityID,
                        principalTable: "Cities",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LoadingUnloadingPoints_Postcodes_PostcodeID",
                        column: x => x.PostcodeID,
                        principalTable: "Postcodes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LoadingUnloadingPoints_Requests_RequestID",
                        column: x => x.RequestID,
                        principalTable: "Requests",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LoadingUnloadingPoints_Companies_SenderRecieverID",
                        column: x => x.SenderRecieverID,
                        principalTable: "Companies",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LoadNumericProps",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 30, nullable: false),
                    Value = table.Column<double>(nullable: false),
                    LoadID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoadNumericProps", x => x.ID);
                    table.ForeignKey(
                        name: "FK_LoadNumericProps_Loads_LoadID",
                        column: x => x.LoadID,
                        principalTable: "Loads",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LoadStringProps",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 30, nullable: false),
                    Value = table.Column<string>(nullable: false),
                    LoadID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoadStringProps", x => x.ID);
                    table.ForeignKey(
                        name: "FK_LoadStringProps_Loads_LoadID",
                        column: x => x.LoadID,
                        principalTable: "Loads",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LoadToLUPoints",
                columns: table => new
                {
                    LoadID = table.Column<int>(nullable: false),
                    LoadingUnloadingPointID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoadToLUPoints", x => new { x.LoadID, x.LoadingUnloadingPointID });
                    table.ForeignKey(
                        name: "FK_LoadToLUPoints_Loads_LoadID",
                        column: x => x.LoadID,
                        principalTable: "Loads",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LoadToLUPoints_LoadingUnloadingPoints_LoadingUnloadingPointID",
                        column: x => x.LoadingUnloadingPointID,
                        principalTable: "LoadingUnloadingPoints",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LoadingUnloadingPoints_CityID",
                table: "LoadingUnloadingPoints",
                column: "CityID");

            migrationBuilder.CreateIndex(
                name: "IX_LoadingUnloadingPoints_PostcodeID",
                table: "LoadingUnloadingPoints",
                column: "PostcodeID");

            migrationBuilder.CreateIndex(
                name: "IX_LoadingUnloadingPoints_RequestID",
                table: "LoadingUnloadingPoints",
                column: "RequestID");

            migrationBuilder.CreateIndex(
                name: "IX_LoadingUnloadingPoints_SenderRecieverID",
                table: "LoadingUnloadingPoints",
                column: "SenderRecieverID");

            migrationBuilder.CreateIndex(
                name: "IX_LoadNumericProps_LoadID",
                table: "LoadNumericProps",
                column: "LoadID");

            migrationBuilder.CreateIndex(
                name: "IX_Loads_PackageTypeID",
                table: "Loads",
                column: "PackageTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Loads_RequestID",
                table: "Loads",
                column: "RequestID");

            migrationBuilder.CreateIndex(
                name: "IX_LoadStringProps_LoadID",
                table: "LoadStringProps",
                column: "LoadID");

            migrationBuilder.CreateIndex(
                name: "IX_LoadToLUPoints_LoadingUnloadingPointID",
                table: "LoadToLUPoints",
                column: "LoadingUnloadingPointID");

            migrationBuilder.CreateIndex(
                name: "IX_Postcodes_CityID",
                table: "Postcodes",
                column: "CityID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LoadNumericProps");

            migrationBuilder.DropTable(
                name: "LoadStringProps");

            migrationBuilder.DropTable(
                name: "LoadToLUPoints");

            migrationBuilder.DropTable(
                name: "Loads");

            migrationBuilder.DropTable(
                name: "LoadingUnloadingPoints");

            migrationBuilder.DropTable(
                name: "PackageTypes");

            migrationBuilder.DropTable(
                name: "Postcodes");

            migrationBuilder.DropColumn(
                name: "DateCreate",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "BirthDate",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Companies");

            migrationBuilder.AddColumn<DateTime>(
                name: "BirhtDate",
                table: "Employees",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Addres",
                table: "Companies",
                type: "nvarchar(120)",
                maxLength: 120,
                nullable: true);
        }
    }
}
