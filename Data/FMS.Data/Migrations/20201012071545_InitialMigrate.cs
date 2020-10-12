using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FMS.Data.Migrations
{
    public partial class InitialMigrate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CompanyTypes",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 30, nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyTypes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "DocumentTypes",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentTypes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Genders",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genders", x => x.ID);
                });

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
                name: "RequestStatuses",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<double>(nullable: false),
                    Name = table.Column<string>(maxLength: 30, nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestStatuses", x => x.ID);
                });

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
                name: "RequestTypes",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 30, nullable: false),
                    Description = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestTypes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Settings",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    CountryID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Cities_Countries_CountryID",
                        column: x => x.CountryID,
                        principalTable: "Countries",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(maxLength: 50, nullable: false),
                    MiddleName = table.Column<string>(maxLength: 50, nullable: false),
                    LastName = table.Column<string>(maxLength: 50, nullable: false),
                    EGN = table.Column<string>(maxLength: 10, nullable: false),
                    BirthDate = table.Column<DateTime>(nullable: false),
                    GenderID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Employees_Genders_GenderID",
                        column: x => x.GenderID,
                        principalTable: "Genders",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Requests",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<string>(nullable: false),
                    DateCreate = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    RequestTypeID = table.Column<int>(nullable: false),
                    RequestStatusID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Requests", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Requests_RequestStatuses_RequestStatusID",
                        column: x => x.RequestStatusID,
                        principalTable: "RequestStatuses",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Requests_RequestTypes_RequestTypeID",
                        column: x => x.RequestTypeID,
                        principalTable: "RequestTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 80, nullable: false),
                    Bulstat = table.Column<string>(maxLength: 15, nullable: true),
                    TaxNumber = table.Column<string>(maxLength: 20, nullable: true),
                    CountryID = table.Column<int>(nullable: false),
                    CityID = table.Column<int>(nullable: false),
                    Address = table.Column<string>(maxLength: 120, nullable: true),
                    CompanyTypeID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Companies_Cities_CityID",
                        column: x => x.CityID,
                        principalTable: "Cities",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Companies_CompanyTypes_CompanyTypeID",
                        column: x => x.CompanyTypeID,
                        principalTable: "CompanyTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Companies_Countries_CountryID",
                        column: x => x.CountryID,
                        principalTable: "Countries",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Postcodes",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(maxLength: 16, nullable: false),
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
                name: "EmployeeBoolProps",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 30, nullable: false),
                    Value = table.Column<bool>(nullable: false),
                    EmployeeID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeBoolProps", x => x.ID);
                    table.ForeignKey(
                        name: "FK_EmployeeBoolProps_Employees_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "Employees",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeNumericProps",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 30, nullable: false),
                    Value = table.Column<double>(nullable: false),
                    EmployeeID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeNumericProps", x => x.ID);
                    table.ForeignKey(
                        name: "FK_EmployeeNumericProps_Employees_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "Employees",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeStringProps",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 30, nullable: false),
                    Value = table.Column<string>(nullable: false),
                    EmployeeID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeStringProps", x => x.ID);
                    table.ForeignKey(
                        name: "FK_EmployeeStringProps_Employees_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "Employees",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Documents",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocumentNumber = table.Column<string>(maxLength: 50, nullable: false),
                    DocumentTypeID = table.Column<int>(nullable: false),
                    RequestID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documents", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Documents_DocumentTypes_DocumentTypeID",
                        column: x => x.DocumentTypeID,
                        principalTable: "DocumentTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Documents_Requests_RequestID",
                        column: x => x.RequestID,
                        principalTable: "Requests",
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
                name: "RequestNumericProps",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Value = table.Column<double>(nullable: false),
                    RequestID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestNumericProps", x => x.ID);
                    table.ForeignKey(
                        name: "FK_RequestNumericProps_Requests_RequestID",
                        column: x => x.RequestID,
                        principalTable: "Requests",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RequestStatusHistories",
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
                    table.PrimaryKey("PK_RequestStatusHistories", x => x.ID);
                    table.ForeignKey(
                        name: "FK_RequestStatusHistories_RequestStatuses_NewStatusID",
                        column: x => x.NewStatusID,
                        principalTable: "RequestStatuses",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RequestStatusHistories_RequestStatuses_OldStatusID",
                        column: x => x.OldStatusID,
                        principalTable: "RequestStatuses",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RequestStatusHistories_Requests_RequestID",
                        column: x => x.RequestID,
                        principalTable: "Requests",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RequestStringProps",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true),
                    RequestID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestStringProps", x => x.ID);
                    table.ForeignKey(
                        name: "FK_RequestStringProps_Requests_RequestID",
                        column: x => x.RequestID,
                        principalTable: "Requests",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
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

            migrationBuilder.CreateTable(
                name: "CompanyBoolProps",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 30, nullable: false),
                    Value = table.Column<bool>(nullable: false),
                    CompanyID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyBoolProps", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CompanyBoolProps_Companies_CompanyID",
                        column: x => x.CompanyID,
                        principalTable: "Companies",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CompanyNumericProps",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 30, nullable: false),
                    Value = table.Column<double>(nullable: false),
                    CompanyID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyNumericProps", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CompanyNumericProps_Companies_CompanyID",
                        column: x => x.CompanyID,
                        principalTable: "Companies",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CompanyStringProps",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 30, nullable: false),
                    Value = table.Column<string>(nullable: false),
                    CompanyID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyStringProps", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CompanyStringProps_Companies_CompanyID",
                        column: x => x.CompanyID,
                        principalTable: "Companies",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
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
                    table.PrimaryKey("PK_RequestToCompanies", x => new { x.RequestID, x.CompanyID, x.RequestToCompanyRelationTypeID });
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
                name: "DocumentBoolProps",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 30, nullable: false),
                    Value = table.Column<bool>(nullable: false),
                    DocumentID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentBoolProps", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DocumentBoolProps_Documents_DocumentID",
                        column: x => x.DocumentID,
                        principalTable: "Documents",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DocumentNumericProps",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 30, nullable: false),
                    Value = table.Column<double>(nullable: false),
                    DocumentID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentNumericProps", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DocumentNumericProps_Documents_DocumentID",
                        column: x => x.DocumentID,
                        principalTable: "Documents",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

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
                name: "DocumentStringProps",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 30, nullable: false),
                    Value = table.Column<string>(nullable: false),
                    DocumentID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentStringProps", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DocumentStringProps_Documents_DocumentID",
                        column: x => x.DocumentID,
                        principalTable: "Documents",
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
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_CountryID",
                table: "Cities",
                column: "CountryID");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_CityID",
                table: "Companies",
                column: "CityID");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_CompanyTypeID",
                table: "Companies",
                column: "CompanyTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_CountryID",
                table: "Companies",
                column: "CountryID");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyBoolProps_CompanyID",
                table: "CompanyBoolProps",
                column: "CompanyID");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyNumericProps_CompanyID",
                table: "CompanyNumericProps",
                column: "CompanyID");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyStringProps_CompanyID",
                table: "CompanyStringProps",
                column: "CompanyID");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentBoolProps_DocumentID",
                table: "DocumentBoolProps",
                column: "DocumentID");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentNumericProps_DocumentID",
                table: "DocumentNumericProps",
                column: "DocumentID");

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

            migrationBuilder.CreateIndex(
                name: "IX_Documents_DocumentTypeID",
                table: "Documents",
                column: "DocumentTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_RequestID",
                table: "Documents",
                column: "RequestID");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentStringProps_DocumentID",
                table: "DocumentStringProps",
                column: "DocumentID");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeBoolProps_EmployeeID",
                table: "EmployeeBoolProps",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeNumericProps_EmployeeID",
                table: "EmployeeNumericProps",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_GenderID",
                table: "Employees",
                column: "GenderID");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeStringProps_EmployeeID",
                table: "EmployeeStringProps",
                column: "EmployeeID");

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

            migrationBuilder.CreateIndex(
                name: "IX_RequestNumericProps_RequestID",
                table: "RequestNumericProps",
                column: "RequestID");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_RequestStatusID",
                table: "Requests",
                column: "RequestStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_RequestTypeID",
                table: "Requests",
                column: "RequestTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_RequestStatusHistories_NewStatusID",
                table: "RequestStatusHistories",
                column: "NewStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_RequestStatusHistories_OldStatusID",
                table: "RequestStatusHistories",
                column: "OldStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_RequestStatusHistories_RequestID",
                table: "RequestStatusHistories",
                column: "RequestID");

            migrationBuilder.CreateIndex(
                name: "IX_RequestStringProps_RequestID",
                table: "RequestStringProps",
                column: "RequestID");

            migrationBuilder.CreateIndex(
                name: "IX_RequestToCompanies_CompanyID",
                table: "RequestToCompanies",
                column: "CompanyID");

            migrationBuilder.CreateIndex(
                name: "IX_RequestToCompanies_RequestToCompanyRelationTypeID",
                table: "RequestToCompanies",
                column: "RequestToCompanyRelationTypeID");

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
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "CompanyBoolProps");

            migrationBuilder.DropTable(
                name: "CompanyNumericProps");

            migrationBuilder.DropTable(
                name: "CompanyStringProps");

            migrationBuilder.DropTable(
                name: "DocumentBoolProps");

            migrationBuilder.DropTable(
                name: "DocumentNumericProps");

            migrationBuilder.DropTable(
                name: "DocumentRowBooleanProps");

            migrationBuilder.DropTable(
                name: "DocumentRowNumericProps");

            migrationBuilder.DropTable(
                name: "DocumentRowStringProps");

            migrationBuilder.DropTable(
                name: "DocumentStringProps");

            migrationBuilder.DropTable(
                name: "EmployeeBoolProps");

            migrationBuilder.DropTable(
                name: "EmployeeNumericProps");

            migrationBuilder.DropTable(
                name: "EmployeeStringProps");

            migrationBuilder.DropTable(
                name: "LoadNumericProps");

            migrationBuilder.DropTable(
                name: "LoadStringProps");

            migrationBuilder.DropTable(
                name: "LoadToLUPoints");

            migrationBuilder.DropTable(
                name: "RequestNumericProps");

            migrationBuilder.DropTable(
                name: "RequestStatusHistories");

            migrationBuilder.DropTable(
                name: "RequestStringProps");

            migrationBuilder.DropTable(
                name: "RequestToCompanies");

            migrationBuilder.DropTable(
                name: "RequestToEmployees");

            migrationBuilder.DropTable(
                name: "Settings");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "DocumentRows");

            migrationBuilder.DropTable(
                name: "Loads");

            migrationBuilder.DropTable(
                name: "LoadingUnloadingPoints");

            migrationBuilder.DropTable(
                name: "RequestToCompanyRelationTypes");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "RequestToEmployeeRelationTypes");

            migrationBuilder.DropTable(
                name: "Documents");

            migrationBuilder.DropTable(
                name: "PackageTypes");

            migrationBuilder.DropTable(
                name: "Postcodes");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "Genders");

            migrationBuilder.DropTable(
                name: "DocumentTypes");

            migrationBuilder.DropTable(
                name: "Requests");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "CompanyTypes");

            migrationBuilder.DropTable(
                name: "RequestStatuses");

            migrationBuilder.DropTable(
                name: "RequestTypes");

            migrationBuilder.DropTable(
                name: "Countries");
        }
    }
}
