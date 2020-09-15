using FMS.Data.Models;
using FMS.Data.Models.Company;
using FMS.Data.Models.Document;
using FMS.Data.Models.Employee;
using FMS.Data.Models.Request;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Reflection;

namespace FMS.Data
{
    public class FMSDBContext : DbContext
    {
        //Request
        public DbSet<Request> Requests { get; set; }
        public DbSet<RequestType> RequestTypes { get; set; }
        public DbSet<RequestToCompany> RequestToCompanies { get; set; }
        public DbSet<RequestToCompanyRelationType> RequestToCompanyRelationTypes { get; set; }
        public DbSet<RequestToEmployee> RequestToEmployees { get; set; }
        public DbSet<RequestToEmployeeRelationType> RequestToEmployeeRelationTypes { get; set; }
        public DbSet<RequestStatus> RequestStatuses { get; set; }
        public DbSet<RequestStatusHistory> RequestStatusHistories { get; set; }
        public DbSet<Load> Loads { get; set; }
        public DbSet<PackageType> PackageTypes { get; set; }
        public DbSet<LoadNumericProps> LoadNumericProps { get; set; }
        public DbSet<LoadStringProp> LoadStringProps { get; set; }
        public DbSet<LoadingUnloadingPoint> LoadingUnloadingPoints { get; set; }
        public DbSet<LoadToLUPoint> LoadToLUPoints { get; set; }

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
        public DbSet<Postcode> Postcodes { get; set; }


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
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
            base.OnModelCreating(builder);
        }
    }
}
