using System.ComponentModel.DataAnnotations;

namespace FMS.Data.Models.Document
{
    public class DocumentRowStringProp
    {
        public int ID { get; set; }
        
        [Required]
        [MaxLength(DataValidation.PropetryNameMaxLenght)]
        public string Name { get; set; }
        
        [Required]
        public string Value { get; set; }
        
        public int DocumentRowID { get; set; }

        public DocumentRow DocumentRow { get; set; }
    }
}
