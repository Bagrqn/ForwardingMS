namespace FMS.Data
{
    using Microsoft.EntityFrameworkCore;
    using FMS.Data.Models;
    using FMS.Data.Models.Company;
    using FMS.Data.Models.Employee;
    using FMS.Data.Models.Document;
    using System.Linq;
    using FMS.Data.Models.Request;

    public class FMSDBContext : DbContext
    {
        //Request
        public DbSet<Request> Requests { get; set; }
        public DbSet<RequestType> RequestTypes { get; set; }

        //Employee
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<EmployeeStringProp> EmployeeStringProps { get; set; }
        public DbSet<EmployeeNumericProp> EmployeeNumericProps { get; set; }
        public DbSet<EmployeeBoolProp> EmployeeBoolProps { get; set; }

        //Company
        public DbSet<Company> Companies { get; set; }
        public DbSet<CompanyType> CompanyTypes { get; set; }
        public DbSet<CompanyStringProp> CompanyStringProps { get; set; }
        public DbSet<CompanyNumericProp> CompanyNumericProps { get; set; }
        public DbSet<CompanyBoolProp> CompanyBoolProps { get; set; }

        //Document
        public DbSet<Document> Documents { get; set; }
        public DbSet<DocumentType> DocumentTypes { get; set; }
        public DbSet<DocumentBoolProp> DocumentBoolProps { get; set; }
        public DbSet<DocumentStringProp> DocumentStringProps { get; set; }
        public DbSet<DocumentNumericProp> DocumentNumericProps { get; set; }


        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //TO DO: Connection string musnt not be here. Must come from app.config or from other comfig. 
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=FMS;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
            base.OnModelCreating(builder);

            builder.Entity<Request>(request =>
            {
                //Request has one RequestType
                request.HasOne(r => r.RequestType)
                .WithMany(rt => rt.Requests)
                .HasForeignKey(r => r.RequestTypeID);

                //Request has one Company Assignor 
                //Each Company assign many requests
                request.HasOne(r => r.CompanyАssignor)
                .WithMany(ca => ca.RequestsAsAssignor)
                .HasForeignKey(r => r.CompanyAssignorID)
                .OnDelete(DeleteBehavior.Restrict);
                
                //Reques has one Comapny Payer
                //Each Company pay many requests
                request.HasOne(r => r.CompanyPayer)
                .WithMany(cp => cp.RequestsAsPayer)
                .HasForeignKey(r => r.CompanyPayerID)
                .OnDelete(DeleteBehavior.Restrict); 
            });

            builder.Entity<Document>(document =>
            {
                //Document with one DocumentType
                document.HasOne(d => d.DocumentType)
                .WithMany(d => d.Documents)
                .HasForeignKey(d => d.DocumentTypeID);

                //Document has many string Props
                document.HasMany(d => d.DocumentStringProps)
                .WithOne(dsp => dsp.Document)
                .HasForeignKey(dsp => dsp.DocumentID);

                //Document has many numeric Props
                document.HasMany(d => d.DocumentNumericProps)
                .WithOne(dnp => dnp.Document)
                .HasForeignKey(dnp => dnp.DocumentID);

                //Document has many boolean Props
                document.HasMany(d => d.DocumentBoolProps)
                .WithOne(dbp => dbp.Document)
                .HasForeignKey(dbp => dbp.DocumentID);

            });

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
                employee.HasMany(e => e.EmployeeNumericProps)
                .WithOne(e => e.Employee)
                .HasForeignKey(e => e.EmployeeID);

                //Employee with many employeeBoolProps
                employee.HasMany(e => e.EmployeeBoolProps)
                .WithOne(e => e.Employee)
                .HasForeignKey(e => e.EmployeeID);

            });

            builder.Entity<Company>(company =>
               {
                   //Company has one Country
                   company.HasOne(c => c.Country)
                   .WithMany(cn => cn.Companies)
                   .HasForeignKey(c => c.CountryID);

                   //Company has one City
                   company.HasOne(c => c.City)
                   .WithMany(ci => ci.Companies)
                   .HasForeignKey(c => c.CityID);

                   //Company has one CompanyType
                   company.HasOne(c => c.CompanyType)
                   .WithMany(c => c.Companies)
                   .HasForeignKey(c => c.CompanyTypeID);

                   //Company with many companyStringProps
                   company.HasMany(c => c.CompanyStringProps)
                   .WithOne(c => c.Company)
                   .HasForeignKey(c => c.CompanyID);

                   //Company with many companyNumericProps
                   company.HasMany(c => c.CompanyNumericProps)
                   .WithOne(c => c.Company)
                   .HasForeignKey(c => c.CompanyID);

                   //Company with many companyBoolProps
                   company.HasMany(c => c.CompanyBoolProps)
                   .WithOne(c => c.Company)
                   .HasForeignKey(c => c.CompanyID);

               });

            builder.Entity<Country>(country =>
            {
                //Country has many Companies
                country.HasMany(c => c.Companies)
                .WithOne(c => c.Country)
                .HasForeignKey(c => c.CountryID);

                //Country has many Cities
                country.HasMany(c => c.Cities)
                .WithOne(c => c.Country)
                .HasForeignKey(c => c.CountryID);

            });

            builder.Entity<City>(city =>
            {
                //City has many Companies
                city.HasMany(c => c.Companies)
                .WithOne(c => c.City)
                .HasForeignKey(c => c.CityID);
            });

            builder.Entity<CompanyType>(compType =>
            {
                //CompanyType has many companies
                compType.HasMany(ct => ct.Companies)
                .WithOne(ct => ct.CompanyType)
                .HasForeignKey(ct => ct.CompanyTypeID);
            });
        }
    }
}
