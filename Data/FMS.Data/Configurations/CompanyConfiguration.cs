using FMS.Data.Models.Company;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FMS.Data.Configurations
{
    public class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> company)
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

        }
    }
}
