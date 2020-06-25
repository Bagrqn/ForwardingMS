﻿namespace FMS.Data
{
    using Microsoft.EntityFrameworkCore;
    using System.Security.Cryptography.X509Certificates;
    using FMS.Data.Models;
    using Microsoft.EntityFrameworkCore.Internal;
    using System.Runtime.InteropServices;
    using System;
    using System.Linq;

    public class FMSDBContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeStringProp> EmployeeStringProps { get; set; }

        public DbSet<Company> Companies { get; set; }
        public DbSet<CompanyType> CompanyTypes { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Gender> Genders { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //TO DO: Connection string musnt not be here. Must come from app.config or from other comfig. 
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=.;Database=FMS;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            base.OnModelCreating(builder);

            builder.Entity<Employee>(employee =>
            {
                //Employee witn one gender
                employee.HasOne(e => e.Gender)
                .WithMany(e => e.Employees)
                .HasForeignKey(e => e.GenderID);

                //Employee with many employeeStringProps
                employee.HasMany(e => e.EmployeeStringProps)
                .WithOne(e => e.Employee)
                .HasForeignKey(e => e.EmployeeID);

                //Employee with many employeeNumericProps
                employee.HasMany(e => e.employeeNumericProps)
                .WithOne(e => e.Employee)
                .HasForeignKey(e => e.EmployeeID);

                //Employee with many employeeBoolProps
                employee.HasMany(e => e.employeeBoolProps)
                .WithOne(e => e.Employee)
                .HasForeignKey(e => e.EmployeeID);

            });

            builder.Entity<Company>(company =>
               {
                   //Company with many companyStringProps
                   company.HasMany(c => c.companyStringProps)
                   .WithOne(c => c.Company)
                   .HasForeignKey(c => c.CompanyId);

                   //Company with many companyNumericProps
                   company.HasMany(c => c.companyNumericProps)
                   .WithOne(c => c.Company)
                   .HasForeignKey(c => c.CompanyId);

                   //Company with many companyBoolProps
                   company.HasMany(c => c.companyBoolProps)
                   .WithOne(c => c.Company)
                   .HasForeignKey(c => c.CompanyId);

               });

            builder.Entity<Country>(country =>
            {
                country.HasMany(c => c.Companies)
                .WithOne(c => c.Country)
                .HasForeignKey(c => c.CountryId);

                country.HasMany(c => c.Cities)
                .WithOne(c => c.Country)
                .HasForeignKey(c => c.CountryID);

            });

            builder.Entity<City>(city =>
            {
                city.HasMany(c => c.Companies)
                .WithOne(c => c.City)
                .HasForeignKey(c => c.CityId);
            });

            builder.Entity<CompanyType>(compType =>
            {
                compType.HasMany(ct => ct.Companies)
                .WithOne(ct => ct.companyType)
                .HasForeignKey(ct => ct.CompanyTypeId);
            });
        }
    }
}
