using System.ComponentModel.DataAnnotations;

namespace FMS.Data.Models.Company
{
    public class CompanyBoolProp
    {
        public int ID { get; set; }

        [Required]
        [MaxLength(DataValidation.PropetryNameMaxLenght)]
        public string Name { get; set; }

        [Required]
        public bool Value { get; set; }

        public int CompanyID { get; set; }

        public Company Company{ get; set; }
    }
}
