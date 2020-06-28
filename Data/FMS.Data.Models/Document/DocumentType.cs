using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FMS.Data.Models.Document
{
    public class DocumentType
    {
        public int ID { get; set; }

        [Required]
        [MaxLength(DataValidation.DocumentTypeNameMaxLenght)]
        public string Name { get; set; }

        public ICollection<Document> Documents { get; set; } = new HashSet<Document>();

    }
}