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

            //LoadingUnloadingPoint
            builder.Entity<LoadingUnloadingPoint>(lupoint =>
            {
                lupoint.HasOne(lup => lup.SenderReciever)
                .WithMany(lup => lup.LoadingUnloadingPoints)
                .HasForeignKey(lup => lup.SenderRecieverID);

                lupoint.HasOne(lup => lup.City)
                .WithMany(c => c.LoadingUnloadingPoints)
                .HasForeignKey(lup => lup.CityID);

                lupoint.HasOne(lup => lup.Postcode)
                .WithMany(pc => pc.LoadingUnloadingPoints)
                .HasForeignKey(lup => lup.PostcodeID);

                lupoint.HasOne(lup => lup.Request)
                .WithMany(r => r.LoadingUnloadingPoints)
                .HasForeignKey(lup => lup.RequestID);
            });

            //Load
            builder.Entity<Load>(load =>
            {
                load.HasOne(l => l.PackageType)
                .WithMany(pt => pt.Loads)
                .HasForeignKey(l => l.PackageTypeID);

                load.HasMany(l => l.LoadNumericProps)
                .WithOne(np => np.Load)
                .HasForeignKey(np => np.LoadID);

                load.HasMany(l => l.LoadStringProps)
                .WithOne(np => np.Load)
                .HasForeignKey(np => np.LoadID);
            });

            //RequestStatusHistory
            builder.Entity<RequestStatusHistory>(requestStatusHistories =>
            {
                requestStatusHistories.HasOne(rh => rh.Request)
                .WithMany(r => r.RequestStatusHistories)
                .HasForeignKey(r => r.RequestID);

                requestStatusHistories.HasOne(rh => rh.OldRequestStatus)
                .WithMany(os => os.OldRequestStatusHistories)
                .HasForeignKey(rh => rh.OldStatusID)
                .OnDelete(DeleteBehavior.Restrict);

                requestStatusHistories.HasOne(rh => rh.NewRequestStatus)
                .WithMany(ns => ns.NewRequestStatusHistories)
                .HasForeignKey(rh => rh.NewStatusID)
                .OnDelete(DeleteBehavior.Restrict);

            });

            //LoadToLUPoint
            builder.Entity<LoadToLUPoint>(loadToLUPoint =>
            {
                loadToLUPoint.HasKey(ltp => new { ltp.LoadID, ltp.LoadingUnloadingPointID });

                loadToLUPoint.HasOne(ltp => ltp.Load)
                .WithMany(l => l.LoadToLUPoints)
                .HasForeignKey(ltp => ltp.LoadID);

                loadToLUPoint.HasOne(ltp => ltp.LoadingUnloadingPoint)
                .WithMany(ltp => ltp.LoadToLUPoints)
                .HasForeignKey(ltp => ltp.LoadingUnloadingPointID);
            });

            //RequestToEmployee
            builder.Entity<RequestToEmployee>(requestToEmployees =>
            {
                //Request to Employee is many to many relation table with additional ingformation Relation type.
                //Relation type explain business sense of the relationship.
                requestToEmployees.HasKey(r => new { r.RequestID, r.EmployeeID });

                requestToEmployees.HasOne(r => r.Request)
                .WithMany(r => r.RequestToEmployees)
                .HasForeignKey(r => r.RequestID);

                requestToEmployees.HasOne(r => r.Employee)
                .WithMany(e => e.RequestToEmployees)
                .HasForeignKey(r => r.EmployeeID);

                requestToEmployees.HasOne(r => r.RequestToEmployeeRelationType)
                .WithMany(rt => rt.RequestToEmployees)
                .HasForeignKey(r => r.RequestToEmployeeRelationTypeID);
            });

            //RequestToCompany
            builder.Entity<RequestToCompany>(requestToCompany =>
            {
                //Request to Company is many to many relation table with additional ingformation Relation type.
                //Relation type explain business sense of the relationship.
                //In this case, we have Company who is a asignor and other or same who is payer. RelationType explane this relationships.
                requestToCompany.HasKey(r => new { r.RequestID, r.CompanyID });

                requestToCompany.HasOne(rtcs => rtcs.Request)
                .WithMany(r => r.RequestToCompanies)
                .HasForeignKey(rtcs => rtcs.RequestID);

                requestToCompany.HasOne(rtcs => rtcs.Company)
                .WithMany(c => c.RequestToCompanies)
                .HasForeignKey(rtcs => rtcs.CompanyID);

                requestToCompany.HasOne(rtc => rtc.RequestToCompanyRelationType)
                .WithMany(rt => rt.RequestToCompanies)
                .HasForeignKey(rtc => rtc.RequestToCompanyRelationTypeID);

            });

            //Request
            builder.Entity<Request>(request =>
            {
                //Request has one RequestType
                request.HasOne(r => r.RequestType)
                .WithMany(rt => rt.Requests)
                .HasForeignKey(r => r.RequestTypeID);

                request.HasOne(r => r.RequestStatus)
                .WithMany(rs => rs.Requests)
                .HasForeignKey(r => r.RequestStatusID);

                //Request HasMany Loads
                request.HasMany(r => r.Loads)
                .WithOne(l => l.Request)
                .HasForeignKey(l => l.RequestID);

                //Request has many LoadingUnloadingPoints
                request.HasMany(r => r.LoadingUnloadingPoints)
                .WithOne(r => r.Request)
                .HasForeignKey(r => r.RequestID);
            });

            //Document
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

            //Employee
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

            //Company
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

            //Country
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

            //City  
            builder.Entity<City>(city =>
            {
                //City has many Companies
                city.HasMany(c => c.Companies)
                .WithOne(c => c.City)
                .HasForeignKey(c => c.CityID);

                //City has many Postcodes
                city.HasMany(c => c.Postcodes)
                .WithOne(p => p.City)
                .HasForeignKey(p => p.CityID);
            });

            //CompanyType
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
