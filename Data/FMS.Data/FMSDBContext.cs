namespace FMS.Data
{
    using Microsoft.EntityFrameworkCore;
    using System.Security.Cryptography.X509Certificates;
    using FMS.Data.Models;
    using Microsoft.EntityFrameworkCore.Internal;

    public class FMSDBContext : DbContext
    {
        public DbSet<PropName> propNames { get; set; }
        public DbSet<BoolProp> BoolProps { get; set; }
        public DbSet<StringProp> StringProps { get; set; }
        public DbSet<NumericProp> NumericProps { get; set; }
        public DbSet<Employee> Employees { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
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

            builder.Entity<Employee>(employee =>
            {
                employee.HasMany(e => e.StringProps)
                .WithOne(strProp => strProp.Employee)
                .HasForeignKey(strProp => strProp.EmployeeId);

                employee.HasMany(e => e.NumericProps)
                .WithOne(numProp => numProp.Employee)
                .HasForeignKey(numProp => numProp.EmployeeId);
            });
        }
    }
}
