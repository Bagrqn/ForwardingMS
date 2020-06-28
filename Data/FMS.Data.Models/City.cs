using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FMS.Data.Models.Company;

namespace FMS.Data.Models
{
    public class City
    {
        public int ID { get; set; }

        [Required]
        [MaxLength(DataValidation.CityNameMaxLenght)]
        public string Name { get; set; }

        public int CountryID { get; set; }

        public Country Country { get; set; }

        public ICollection<Company.Company> Companies { get; set; } = new HashSet<Company.Company>();
    }
}
