using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FMS.Data.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string MiddleName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [MaxLength(10)]
        public string EGN { get; set; }

        public DateTime BirhtDate { get; set; }

        public int GenderID { get; set; }

        public Gender Gender { get; set; }

        public ICollection<StringProp> StringProps { get; set; } = new HashSet<StringProp>();

        public ICollection<NumericProp> NumericProps { get; set; } = new HashSet<NumericProp>();

    }
}
