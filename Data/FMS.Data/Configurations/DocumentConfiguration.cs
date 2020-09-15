using FMS.Data.Models.Document;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FMS.Data.Configurations
{
    public class DocumentConfiguration : IEntityTypeConfiguration<Document>
    {
        public void Configure(EntityTypeBuilder<Document> document)
        {
            //Document with one DocumentType
            document.HasOne(d => d.DocumentType)
            .WithMany(d => d.Documents)
            .HasForeignKey(d => d.DocumentTypeID);

            //Document has many string Props
            document.HasMany(d => d.DocumentStringProps)
            .WithOne(dsp => dsp.Document)
            .HasForeignKey(dsp => dsp.DocumentID);

            //Document has many numeric Props
            document.HasMany(d => d.DocumentNumericProps)
            .WithOne(dnp => dnp.Document)
            .HasForeignKey(dnp => dnp.DocumentID);

            //Document has many boolean Props
            document.HasMany(d => d.DocumentBoolProps)
            .WithOne(dbp => dbp.Document)
            .HasForeignKey(dbp => dbp.DocumentID);
        }
    }
}
