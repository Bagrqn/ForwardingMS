using FMS.Data.Models.Request;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FMS.Data.Configurations
{
    public class LoadingUnloadingPointConfiguration : IEntityTypeConfiguration<LoadingUnloadingPoint>
    {
        public void Configure(EntityTypeBuilder<LoadingUnloadingPoint> lupoint)
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
        }
    }
}
