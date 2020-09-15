using FMS.Data.Models.Request;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FMS.Data.Configurations
{
    public class RequestStatusHistoryConfiguration : IEntityTypeConfiguration<RequestStatusHistory>
    {
        public void Configure(EntityTypeBuilder<RequestStatusHistory> requestStatusHistories)
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
        }
    }
}
