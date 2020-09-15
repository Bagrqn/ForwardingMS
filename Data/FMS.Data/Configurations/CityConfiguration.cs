using FMS.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FMS.Data.Configurations
{
    public class CityConfiguration : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> city)
        {
            //City has many Companies
            city.HasMany(c => c.Companies)
            .WithOne(c => c.City)
            .HasForeignKey(c => c.CityID);

            //City has many Postcodes
            city.HasMany(c => c.Postcodes)
            .WithOne(p => p.City)
            .HasForeignKey(p => p.CityID);
        }
    }
}
