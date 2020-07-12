using FMS.Data.Models.Request;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

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

        public ICollection<Postcode> Postcodes { get; set; } = new HashSet<Postcode>();

        public ICollection<LoadingUnloadingPoint> LoadingUnloadingPoints { get; set; } = new HashSet<LoadingUnloadingPoint>();
    }
}
