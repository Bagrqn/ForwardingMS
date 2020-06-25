using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FMS.Data.Models
{
    public class EmployeeStringProp
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(DataValidation.PropetryNameMaxLenght)]
        public string Name { get; set; }

        public string Value { get; set; }
        public int EmployeeID { get; set; }

        public Employee Employee { get; set; }
    }
}
