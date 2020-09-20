using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FMS.Data.Models.Request
{
    public class Load
    {
        public int ID { get; set; }

        [Required]
        [MaxLength(DataValidation.NameMaxLenght)]
        public string Name { get; set; }

        public string Comment { get; set; }

        public int PackageTypeID { get; set; }

        public PackageType PackageType { get; set; }

        public int PackageCount { get; set; }

        public int RequestID { get; set; }

        public Request Request { get; set; }

        public ICollection<LoadNumericProps> LoadNumericProps { get; set; } = new HashSet<LoadNumericProps>();

        public ICollection<LoadStringProp> LoadStringProps { get; set; } = new HashSet<LoadStringProp>();

        public ICollection<LoadToLUPoint> LoadToLUPoints { get; set; } = new HashSet<LoadToLUPoint>();
    }
}