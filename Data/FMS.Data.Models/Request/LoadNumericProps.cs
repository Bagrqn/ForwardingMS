using System.ComponentModel.DataAnnotations;

namespace FMS.Data.Models.Request
{
    public class LoadNumericProps
    {
        public int ID { get; set; }

        [Required]
        [MaxLength(DataValidation.PropetryNameMaxLenght)]
        public string Name { get; set; }

        [Required]
        public double Value { get; set; }

        public int LoadID { get; set; }

        public Load Load { get; set; }
    }
}
