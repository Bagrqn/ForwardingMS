using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;

namespace FMS.Data.Models
{
    public class Gender
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();
    }
}