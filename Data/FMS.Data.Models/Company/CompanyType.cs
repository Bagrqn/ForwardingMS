using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FMS.Data.Models.Company
{
    public class CompanyType
    {
        public int ID { get; set; }

        [Required]
        [MaxLength(DataValidation.CompanyTypeNameMaxLenght)]
        public string Name { get; set; }

        public string Description { get; set; }

        public ICollection<Company> Companies { get; set; }
    }
}
