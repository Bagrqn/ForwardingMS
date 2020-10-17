using FMS.Data.Models.Document;
using System.Collections.Generic;

namespace FMS.Services.Models.Document
{
    public class DocumentRowListingServiceModel
    {
        public int RowID { get; set; }
        public int RowNumber { get; set; }
        public List<DocumentRowStringProp> DocumentRowStringProps { get; set; } = new List<DocumentRowStringProp>();
        public List<DocumentRowNumericProp> DocumentRowNumericProps { get; set; } = new List<DocumentRowNumericProp>();
        public List<DocumentRowBooleanProp> DocumentRowBooleanProps { get; set; } = new List<DocumentRowBooleanProp>();

    }
}
