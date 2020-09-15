using FMS.Data.Models.Request;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FMS.Data.Configurations
{
    public class RequestToEmployeeConfiguration : IEntityTypeConfiguration<RequestToEmployee>
    {
        public void Configure(EntityTypeBuilder<RequestToEmployee> requestToEmployees)
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
        }
    }
}
