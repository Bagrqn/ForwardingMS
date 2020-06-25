using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FMS.Data.Models
{
    public class CompanyBoolProp
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(DataValidation.PropetryNameMaxLenght)]
        public string Name { get; set; }

        [Required]
        public bool Value { get; set; }

        public int CompanyId { get; set; }

        public Company Company{ get; set; }
    }
}
