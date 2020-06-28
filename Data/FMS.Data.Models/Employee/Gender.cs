using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FMS.Data.Models.Employee
{
    public class Gender
    {
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();
    }
}