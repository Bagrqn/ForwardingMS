using System.ComponentModel.DataAnnotations;

namespace FMS.Data.Models.Document
{
    public class DocumentNumericProp
    {
        public int ID { get; set; }

        [Required]
        [MaxLength(DataValidation.PropetryNameMaxLenght)]
        public string Name { get; set; }

        [Required]
        public double Value { get; set; }

        public int DocumentID { get; set; }

        public Document Document { get; set; }
    }
}