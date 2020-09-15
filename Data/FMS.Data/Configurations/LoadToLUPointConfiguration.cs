using FMS.Data.Models.Request;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FMS.Data.Configurations
{
    public class LoadToLUPointConfiguration : IEntityTypeConfiguration<LoadToLUPoint>
    {
        public void Configure(EntityTypeBuilder<LoadToLUPoint> loadToLUPoint)
        {
            loadToLUPoint.HasKey(ltp => new { ltp.LoadID, ltp.LoadingUnloadingPointID });

            loadToLUPoint.HasOne(ltp => ltp.Load)
            .WithMany(l => l.LoadToLUPoints)
            .HasForeignKey(ltp => ltp.LoadID);

            loadToLUPoint.HasOne(ltp => ltp.LoadingUnloadingPoint)
            .WithMany(ltp => ltp.LoadToLUPoints)
            .HasForeignKey(ltp => ltp.LoadingUnloadingPointID);
        }
    }
}
