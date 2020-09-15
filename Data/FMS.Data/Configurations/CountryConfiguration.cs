using FMS.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FMS.Data.Configurations
{
    public class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> country)
        {
            //Country has many Companies
            country.HasMany(c => c.Companies)
            .WithOne(c => c.Country)
            .HasForeignKey(c => c.CountryID);

            //Country has many Cities
            country.HasMany(c => c.Cities)
            .WithOne(c => c.Country)
            .HasForeignKey(c => c.CountryID);

        }
    }
}
