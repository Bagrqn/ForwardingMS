using FMS.Data.Models.Request;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FMS.Data.Configurations
{
    public class LoadConfiguration : IEntityTypeConfiguration<Load>
    {
        public void Configure(EntityTypeBuilder<Load> load)
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
        }
    }
}
