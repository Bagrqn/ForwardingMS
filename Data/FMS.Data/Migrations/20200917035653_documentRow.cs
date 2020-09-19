using Microsoft.EntityFrameworkCore.Migrations;

namespace FMS.Data.Migrations
{
    public partial class documentRow : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DocumentRows",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RowNumber = table.Column<int>(nullable: false),
                    DocumentID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentRows", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DocumentRows_Documents_DocumentID",
                        column: x => x.DocumentID,
                        principalTable: "Documents",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DocumentRowBooleanProps",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 30, nullable: false),
                    Value = table.Column<bool>(nullable: false),
                    DocumentRowID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentRowBooleanProps", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DocumentRowBooleanProps_DocumentRows_DocumentRowID",
                        column: x => x.DocumentRowID,
                        principalTable: "DocumentRows",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DocumentRowNumericProps",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 30, nullable: false),
                    Value = table.Column<double>(nullable: false),
                    DocumentRowID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentRowNumericProps", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DocumentRowNumericProps_DocumentRows_DocumentRowID",
                        column: x => x.DocumentRowID,
                        principalTable: "DocumentRows",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DocumentRowStringProps",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 30, nullable: false),
                    Value = table.Column<string>(nullable: false),
                    DocumentRowID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentRowStringProps", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DocumentRowStringProps_DocumentRows_DocumentRowID",
                        column: x => x.DocumentRowID,
                        principalTable: "DocumentRows",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DocumentRowBooleanProps_DocumentRowID",
                table: "DocumentRowBooleanProps",
                column: "DocumentRowID");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentRowNumericProps_DocumentRowID",
                table: "DocumentRowNumericProps",
                column: "DocumentRowID");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentRows_DocumentID",
                table: "DocumentRows",
                column: "DocumentID");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentRowStringProps_DocumentRowID",
                table: "DocumentRowStringProps",
                column: "DocumentRowID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DocumentRowBooleanProps");

            migrationBuilder.DropTable(
                name: "DocumentRowNumericProps");

            migrationBuilder.DropTable(
                name: "DocumentRowStringProps");

            migrationBuilder.DropTable(
                name: "DocumentRows");
        }
    }
}
