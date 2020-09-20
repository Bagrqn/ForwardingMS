using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

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
