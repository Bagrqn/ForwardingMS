using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FMS.Data.Models
{
    public class CompanyStringProp
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(DataValidation.PropetryNameMaxLenght)]
        public string Name { get; set; }

        public string Value { get; set; }
        public int CompanyId { get; set; }

        public Company Company { get; set; }
    }
}
