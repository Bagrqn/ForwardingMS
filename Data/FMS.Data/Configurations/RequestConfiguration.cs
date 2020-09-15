using FMS.Data.Models.Request;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FMS.Data.Configurations
{
    public class RequestConfiguration : IEntityTypeConfiguration<Request>
    {
        public void Configure(EntityTypeBuilder<Request> request)
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

        }
    }
}
