using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FMS.Data.Models.Request
{
    public class PackageType
    {
        public int ID { get; set; }
        
        [Required]
        [MaxLength(DataValidation.NameMaxLenght)]
        public string TypeName { get; set; }

        public ICollection<Load> Loads { get; set; } = new HashSet<Load>();
    }
}