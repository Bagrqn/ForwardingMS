using System.ComponentModel.DataAnnotations;

namespace FMS.Data.Models.Document
{
    public class DocumentBoolProp
    {
        public int ID { get; set; }

        [Required]
        [MaxLength(DataValidation.PropetryNameMaxLenght)]
        public string Name { get; set; }

        [Required]
        public bool Value { get; set; }

        public int DocumentID { get; set; }

        public Document Document { get; set; }
    }
}