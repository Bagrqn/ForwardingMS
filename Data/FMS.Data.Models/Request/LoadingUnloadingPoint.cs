using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FMS.Data.Models.Request
{
    public class LoadingUnloadingPoint
    {
        public int ID { get; set; }

        public LoadingUnloadingPointsTypeEnum Type { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public int SenderRecieverID { get; set; }

        public Company.Company SenderReciever { get; set; }

        public ICollection<LoadToLUPoint> LoadToLUPoints { get; set; } = new HashSet<LoadToLUPoint>();

        public int CityID { get; set; }

        public City City { get; set; }

        public int PostcodeID { get; set; }

        public Postcode Postcode { get; set; }

        [Required]
        [MaxLength(DataValidation.AddressMaxLenght)]
        public string Address { get; set; }

        public int RequestID { get; set; }

        public Request Request { get; set; }
    }

    public enum LoadingUnloadingPointsTypeEnum
    {
        Loading = 0,
        Unloading = 1,
    }
}
