﻿// <auto-generated />
using System;
using FMS.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FMS.Data.Migrations
{
    [DbContext(typeof(FMSDBContext))]
    [Migration("20200628144713_Request-Assignor-Added")]
    partial class RequestAssignorAdded
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FMS.Data.Models.City", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CountryID")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("ID");

                    b.HasIndex("CountryID");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("FMS.Data.Models.Company.Company", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Addres")
                        .HasColumnType("nvarchar(120)")
                        .HasMaxLength(120);

                    b.Property<string>("Bulstat")
                        .HasColumnType("nvarchar(15)")
                        .HasMaxLength(15);

                    b.Property<int>("CityID")
                        .HasColumnType("int");

                    b.Property<int>("CompanyTypeID")
                        .HasColumnType("int");

                    b.Property<int>("CountryID")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(80)")
                        .HasMaxLength(80);

                    b.Property<string>("TaxNumber")
                        .HasColumnType("nvarchar(20)")
                        .HasMaxLength(20);

                    b.HasKey("ID");

                    b.HasIndex("CityID");

                    b.HasIndex("CompanyTypeID");

                    b.HasIndex("CountryID");

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("FMS.Data.Models.Company.CompanyBoolProp", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CompanyID")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(30)")
                        .HasMaxLength(30);

                    b.Property<bool>("Value")
                        .HasColumnType("bit");

                    b.HasKey("ID");

                    b.HasIndex("CompanyID");

                    b.ToTable("CompanyBoolProps");
                });

            modelBuilder.Entity("FMS.Data.Models.Company.CompanyNumericProp", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CompanyID")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(30)")
                        .HasMaxLength(30);

                    b.Property<double>("Value")
                        .HasColumnType("float");

                    b.HasKey("ID");

                    b.HasIndex("CompanyID");

                    b.ToTable("CompanyNumericProps");
                });

            modelBuilder.Entity("FMS.Data.Models.Company.CompanyStringProp", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CompanyID")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(30)")
                        .HasMaxLength(30);

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("CompanyID");

                    b.ToTable("CompanyStringProps");
                });

            modelBuilder.Entity("FMS.Data.Models.Company.CompanyType", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(30)")
                        .HasMaxLength(30);

                    b.HasKey("ID");

                    b.ToTable("CompanyTypes");
                });

            modelBuilder.Entity("FMS.Data.Models.Country", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("ID");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("FMS.Data.Models.Document.Document", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("DocumentNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<int>("DocumentTypeID")
                        .HasColumnType("int");

                    b.Property<int>("RequestID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("DocumentTypeID");

                    b.HasIndex("RequestID");

                    b.ToTable("Documents");
                });

            modelBuilder.Entity("FMS.Data.Models.Document.DocumentBoolProp", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DocumentID")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(30)")
                        .HasMaxLength(30);

                    b.Property<bool>("Value")
                        .HasColumnType("bit");

                    b.HasKey("ID");

                    b.HasIndex("DocumentID");

                    b.ToTable("DocumentBoolProps");
                });

            modelBuilder.Entity("FMS.Data.Models.Document.DocumentNumericProp", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DocumentID")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(30)")
                        .HasMaxLength(30);

                    b.Property<double>("Value")
                        .HasColumnType("float");

                    b.HasKey("ID");

                    b.HasIndex("DocumentID");

                    b.ToTable("DocumentNumericProps");
                });

            modelBuilder.Entity("FMS.Data.Models.Document.DocumentStringProp", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DocumentID")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(30)")
                        .HasMaxLength(30);

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("DocumentID");

                    b.ToTable("DocumentStringProps");
                });

            modelBuilder.Entity("FMS.Data.Models.Document.DocumentType", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(30)")
                        .HasMaxLength(30);

                    b.HasKey("ID");

                    b.ToTable("DocumentTypes");
                });

            modelBuilder.Entity("FMS.Data.Models.Employee.Employee", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("BirhtDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("EGN")
                        .IsRequired()
                        .HasColumnType("nvarchar(10)")
                        .HasMaxLength(10);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<int>("GenderID")
                        .HasColumnType("int");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("MiddleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("ID");

                    b.HasIndex("GenderID");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("FMS.Data.Models.Employee.EmployeeBoolProp", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("EmployeeID")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(30)")
                        .HasMaxLength(30);

                    b.Property<bool>("Value")
                        .HasColumnType("bit");

                    b.HasKey("ID");

                    b.HasIndex("EmployeeID");

                    b.ToTable("EmployeeBoolProps");
                });

            modelBuilder.Entity("FMS.Data.Models.Employee.EmployeeNumericProp", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("EmployeeID")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(30)")
                        .HasMaxLength(30);

                    b.Property<double>("Value")
                        .HasColumnType("float");

                    b.HasKey("ID");

                    b.HasIndex("EmployeeID");

                    b.ToTable("EmployeeNumericProps");
                });

            modelBuilder.Entity("FMS.Data.Models.Employee.EmployeeStringProp", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("EmployeeID")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(30)")
                        .HasMaxLength(30);

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("EmployeeID");

                    b.ToTable("EmployeeStringProps");
                });

            modelBuilder.Entity("FMS.Data.Models.Employee.Gender", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Genders");
                });

            modelBuilder.Entity("FMS.Data.Models.Request.Request", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CompanyAssignorID")
                        .HasColumnType("int");

                    b.Property<int>("CompanyPayerID")
                        .HasColumnType("int");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RequestTypeID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("CompanyAssignorID");

                    b.HasIndex("CompanyPayerID");

                    b.HasIndex("RequestTypeID");

                    b.ToTable("Requests");
                });

            modelBuilder.Entity("FMS.Data.Models.Request.RequestType", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(30)")
                        .HasMaxLength(30);

                    b.HasKey("ID");

                    b.ToTable("RequestTypes");
                });

            modelBuilder.Entity("FMS.Data.Models.City", b =>
                {
                    b.HasOne("FMS.Data.Models.Country", "Country")
                        .WithMany("Cities")
                        .HasForeignKey("CountryID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("FMS.Data.Models.Company.Company", b =>
                {
                    b.HasOne("FMS.Data.Models.City", "City")
                        .WithMany("Companies")
                        .HasForeignKey("CityID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("FMS.Data.Models.Company.CompanyType", "CompanyType")
                        .WithMany("Companies")
                        .HasForeignKey("CompanyTypeID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("FMS.Data.Models.Country", "Country")
                        .WithMany("Companies")
                        .HasForeignKey("CountryID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("FMS.Data.Models.Company.CompanyBoolProp", b =>
                {
                    b.HasOne("FMS.Data.Models.Company.Company", "Company")
                        .WithMany("CompanyBoolProps")
                        .HasForeignKey("CompanyID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("FMS.Data.Models.Company.CompanyNumericProp", b =>
                {
                    b.HasOne("FMS.Data.Models.Company.Company", "Company")
                        .WithMany("CompanyNumericProps")
                        .HasForeignKey("CompanyID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("FMS.Data.Models.Company.CompanyStringProp", b =>
                {
                    b.HasOne("FMS.Data.Models.Company.Company", "Company")
                        .WithMany("CompanyStringProps")
                        .HasForeignKey("CompanyID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("FMS.Data.Models.Document.Document", b =>
                {
                    b.HasOne("FMS.Data.Models.Document.DocumentType", "DocumentType")
                        .WithMany("Documents")
                        .HasForeignKey("DocumentTypeID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("FMS.Data.Models.Request.Request", "Request")
                        .WithMany("Documents")
                        .HasForeignKey("RequestID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("FMS.Data.Models.Document.DocumentBoolProp", b =>
                {
                    b.HasOne("FMS.Data.Models.Document.Document", "Document")
                        .WithMany("DocumentBoolProps")
                        .HasForeignKey("DocumentID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("FMS.Data.Models.Document.DocumentNumericProp", b =>
                {
                    b.HasOne("FMS.Data.Models.Document.Document", "Document")
                        .WithMany("DocumentNumericProps")
                        .HasForeignKey("DocumentID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("FMS.Data.Models.Document.DocumentStringProp", b =>
                {
                    b.HasOne("FMS.Data.Models.Document.Document", "Document")
                        .WithMany("DocumentStringProps")
                        .HasForeignKey("DocumentID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("FMS.Data.Models.Employee.Employee", b =>
                {
                    b.HasOne("FMS.Data.Models.Employee.Gender", "Gender")
                        .WithMany("Employees")
                        .HasForeignKey("GenderID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("FMS.Data.Models.Employee.EmployeeBoolProp", b =>
                {
                    b.HasOne("FMS.Data.Models.Employee.Employee", "Employee")
                        .WithMany("EmployeeBoolProps")
                        .HasForeignKey("EmployeeID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("FMS.Data.Models.Employee.EmployeeNumericProp", b =>
                {
                    b.HasOne("FMS.Data.Models.Employee.Employee", "Employee")
                        .WithMany("EmployeeNumericProps")
                        .HasForeignKey("EmployeeID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("FMS.Data.Models.Employee.EmployeeStringProp", b =>
                {
                    b.HasOne("FMS.Data.Models.Employee.Employee", "Employee")
                        .WithMany("EmployeeStringProps")
                        .HasForeignKey("EmployeeID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("FMS.Data.Models.Request.Request", b =>
                {
                    b.HasOne("FMS.Data.Models.Company.Company", "CompanyАssignor")
                        .WithMany("RequestsAsAssignor")
                        .HasForeignKey("CompanyAssignorID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FMS.Data.Models.Company.Company", "CompanyPayer")
                        .WithMany("RequestsAsPayer")
                        .HasForeignKey("CompanyPayerID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("FMS.Data.Models.Request.RequestType", "RequestType")
                        .WithMany("Requests")
                        .HasForeignKey("RequestTypeID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}