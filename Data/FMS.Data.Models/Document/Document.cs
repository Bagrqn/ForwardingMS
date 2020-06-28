using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FMS.Data.Models.Document
{
    public class Document
    {
        public int ID { get; set; }

        [Required]
        [MaxLength(DataValidation.DocumentNumberMaxLenght)]
        public string DocumentNumber { get; set; }

        public int DocumentTypeID { get; set; }

        public DocumentType DocumentType { get; set; }

        public ICollection<DocumentStringProp> DocumentStringProps { get; set; }

        public ICollection<DocumentNumericProp> DocumentNumericProps { get; set; }

        public ICollection<DocumentBoolProp> DocumentBoolProps { get; set; }

        public int RequestID { get; set; }

        public Request.Request Request { get; set; }
    }
}
