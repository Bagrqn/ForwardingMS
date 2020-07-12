using FMS.Data.Models.Request;
using System.Collections.Generic;

namespace FMS.Data.Models
{
    public class Postcode
    {
        public int ID { get; set; }

        public string Code { get; set; }

        public int CityID { get; set; }

        public City City { get; set; }

        public ICollection<LoadingUnloadingPoint> LoadingUnloadingPoints { get; set; } = new HashSet<LoadingUnloadingPoint>();
    }
}
