using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FMS.Data.Models
{
    public class Country
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public ICollection<City> Cities { get; set; } = new HashSet<City>();

        public ICollection<Company> Companies { get; set; } = new HashSet<Company>();
    }
}
