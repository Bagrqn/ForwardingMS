using System.ComponentModel.DataAnnotations;

namespace FMS.Data.Models.Employee
{
   public class EmployeeBoolProp
    {
        public int ID { get; set; }
        
        [Required]
        [MaxLength(DataValidation.PropetryNameMaxLenght)]
        public string Name { get; set; }

        [Required]
        public bool Value { get; set; }

        public int EmployeeID { get; set; }

        public Employee Employee { get; set; }
    }
}
