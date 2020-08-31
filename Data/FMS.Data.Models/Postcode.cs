using FMS.Data.Models.Request;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FMS.Data.Models
{
    public class Postcode
    {
        public int ID { get; set; }

        [Required]
        [MaxLength(DataValidation.PostcodeMaxLenght)]
        public string Code { get; set; }

        public int CityID { get; set; }

        public City City { get; set; }

        public ICollection<LoadingUnloadingPoint> LoadingUnloadingPoints { get; set; } = new HashSet<LoadingUnloadingPoint>();
    }
}
