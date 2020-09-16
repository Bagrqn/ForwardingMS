using System.Collections.Generic;

namespace FMS.Data.Models.Document
{
    public class DocumentRow
    {
        public int ID { get; set; }

        public int RowNumber { get; set; }

        public int DocumentID { get; set; }

        public Document Document { get; set; }

        public ICollection<DocumentRowStringProp> DocumentRowStringProps { get; set; } = new HashSet<DocumentRowStringProp>();

        public ICollection<DocumentRowBooleanProp> DocumentRowBooleanProps { get; set; } = new HashSet<DocumentRowBooleanProp>();

        public ICollection<DocumentRowNumericProp> DocumentRowNumericProps { get; set; } = new HashSet<DocumentRowNumericProp>();
    }
}
