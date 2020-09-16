using FMS.Data.Models.Document;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FMS.Data.Configurations
{
    public class DocumentRowConfiguration : IEntityTypeConfiguration<DocumentRow>
    {
        public void Configure(EntityTypeBuilder<DocumentRow> docRow)
        {
            //DocumentRow has many string props 
            docRow.HasMany(dr => dr.DocumentRowStringProps)
                .WithOne(d => d.DocumentRow)
                .HasForeignKey(d => d.DocumentRowID);

            //documentRow has many boolean props
            docRow.HasMany(dr => dr.DocumentRowBooleanProps)
                .WithOne(d => d.DocumentRow)
                .HasForeignKey(d => d.DocumentRowID);

            //DocumentROW has many numerics props
            docRow.HasMany(dr => dr.DocumentRowNumericProps)
                .WithOne(d => d.DocumentRow)
                .HasForeignKey(d => d.DocumentRowID);
        }
    }
}
