using System.ComponentModel.DataAnnotations;

namespace FMS.Data.Models.Request
{
    public class LoadStringProp
    {
        public int ID { get; set; }

        [Required]
        [MaxLength(DataValidation.PropetryNameMaxLenght)]
        public string Name { get; set; }

        [Required]
        public string Value { get; set; }

        public int LoadID { get; set; }

        public Load Load { get; set; }
    }
}
