using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FMS.Data.Models.Company;

namespace FMS.Data.Models
{
    public class Country
    {
        public int ID { get; set; }

        [Required]
        [MaxLength(DataValidation.CountryNameMaxLenght)]
        public string Name { get; set; }

        public ICollection<City> Cities { get; set; } = new HashSet<City>();

        public ICollection<Company.Company> Companies { get; set; } = new HashSet<Company.Company>();
    }
}
