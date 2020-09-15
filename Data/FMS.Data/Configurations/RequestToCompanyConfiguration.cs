using FMS.Data.Models.Request;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FMS.Data.Configurations
{
    public class RequestToCompanyConfiguration : IEntityTypeConfiguration<RequestToCompany>
    {
        public void Configure(EntityTypeBuilder<RequestToCompany> requestToCompany)
        {
            //Request to Company is many to many relation table with additional ingformation Relation type.
            //Relation type explain business sense of the relationship.
            //In this case, we have Company who is a asignor and other or same who is payer. RelationType explane this relationships.
            requestToCompany.HasKey(r => new { r.RequestID, r.CompanyID });

            requestToCompany.HasOne(rtcs => rtcs.Request)
            .WithMany(r => r.RequestToCompanies)
            .HasForeignKey(rtcs => rtcs.RequestID);

            requestToCompany.HasOne(rtcs => rtcs.Company)
            .WithMany(c => c.RequestToCompanies)
            .HasForeignKey(rtcs => rtcs.CompanyID);

            requestToCompany.HasOne(rtc => rtc.RequestToCompanyRelationType)
            .WithMany(rt => rt.RequestToCompanies)
            .HasForeignKey(rtc => rtc.RequestToCompanyRelationTypeID);
        }
    }
}
