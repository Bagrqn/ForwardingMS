using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FMS.Data.Models
{
    public class EmployeeNumericProp
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(DataValidation.PropetryNameMaxLenght)]
        public string Name { get; set; }
        
        [Required]
        public double Value { get; set; }

        public int EmployeeID { get; set; }

        public Employee Employee { get; set; }
    }
}
