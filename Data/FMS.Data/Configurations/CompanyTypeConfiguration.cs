using FMS.Data.Models.Company;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FMS.Data.Configurations
{
    public class CompanyTypeConfiguration : IEntityTypeConfiguration<CompanyType>
    {
        public void Configure(EntityTypeBuilder<CompanyType> compType)
        {
            //CompanyType has many companies
            compType.HasMany(ct => ct.Companies)
            .WithOne(ct => ct.CompanyType)
            .HasForeignKey(ct => ct.CompanyTypeID);
        }
    }
}
