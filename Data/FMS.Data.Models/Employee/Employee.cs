using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FMS.Data.Models.Employee
{
    public class Employee
    {
        public int ID { get; set; }

        [Required]
        [MaxLength(DataValidation.EmployeeNameMaxLenght)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(DataValidation.EmployeeNameMaxLenght)]
        public string MiddleName { get; set; }

        [Required]
        [MaxLength(DataValidation.EmployeeNameMaxLenght)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(DataValidation.EmployeeEGNMaxLenght)]
        public string EGN { get; set; }

        public DateTime BirhtDate { get; set; }

        public int GenderID { get; set; }

        public Gender Gender { get; set; }

        public ICollection<EmployeeStringProp> EmployeeStringProps { get; set; } = new HashSet<EmployeeStringProp>();
        
        public ICollection<EmployeeNumericProp> EmployeeNumericProps { get; set; } = new HashSet<EmployeeNumericProp>();
        
        public ICollection<EmployeeBoolProp> EmployeeBoolProps { get; set; } = new HashSet<EmployeeBoolProp>();
        
        public ICollection<Request.Request> Requests { get; set; } = new HashSet<Request.Request>();
    }
}
