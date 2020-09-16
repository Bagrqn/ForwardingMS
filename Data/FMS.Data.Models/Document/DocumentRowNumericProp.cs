using System.ComponentModel.DataAnnotations;

namespace FMS.Data.Models.Document
{
    public class DocumentRowNumericProp
    {
        public int ID { get; set; }

        [Required]
        [MaxLength(DataValidation.PropetryNameMaxLenght)]
        public string Name { get; set; }

        public double Value { get; set; }

        public int DocumentRowID { get; set; }

        public DocumentRow DocumentRow { get; set; }
    }
}
