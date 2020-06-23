﻿namespace FMS.Data
{
    using Microsoft.EntityFrameworkCore;
    using System.Security.Cryptography.X509Certificates;
    using FMS.Data.Models;
    using Microsoft.EntityFrameworkCore.Internal;
    using System.Runtime.InteropServices;

    public class FMSDBContext : DbContext
    {
        public DbSet<PropName> propNames { get; set; }
        public DbSet<BoolProp> BoolProps { get; set; }
        public DbSet<StringProp> StringProps { get; set; }
        public DbSet<NumericProp> NumericProps { get; set; }
        public DbSet<Employee> Employees { get; set; }
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
                optionsBuilder.UseSqlServer("Server=.;Database=FMS;Integrated Security=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            #region PropClasses
            builder.Entity<PropName>(prName =>
            {
                prName.HasMany(prp => prp.BoolProps)
                .WithOne(prp => prp.PropName)
                .HasForeignKey(prp => prp.PropNameID);

                prName.HasMany(prp => prp.NumericProps)
                .WithOne(prp => prp.propName)
                .HasForeignKey(prp => prp.PropNameID);

                prName.HasMany(prp => prp.StringProps)
                .WithOne(prp => prp.PropName)
                .HasForeignKey(prp => prp.PropNameId);
            });
            #endregion

            builder.Entity<Employee>(employee =>
            {
                employee.HasMany(e => e.StringProps)
                .WithOne(strProp => strProp.Employee)
                .HasForeignKey(strProp => strProp.EmployeeId);

                employee.HasMany(e => e.NumericProps)
                .WithOne(numProp => numProp.Employee)
                .HasForeignKey(numProp => numProp.EmployeeId);

                employee.HasOne(e => e.Gender)
                .WithMany(e => e.Employees)
                .HasForeignKey(e => e.GenderID);
            });

            builder.Entity<Company>(company =>
            {
                company.HasMany(c => c.StringProps)
                .WithOne(c => c.Company)
                .HasForeignKey(c => c.CompanyId);

                company.HasMany(c => c.NumericProps)
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
